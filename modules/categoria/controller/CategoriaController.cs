using AutoMapper;
using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.categoria.model.request;
using controle_vendas.modules.categoria.model.response;
using controle_vendas.modules.categoria.service.interfaces;
using controle_vendas.modules.common.pagination;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace controle_vendas.modules.categoria.controller;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;
    private readonly IMapper _mapper;

    public CategoriaController(ICategoriaService categoriaService, IMapper mapper)
    {
        _categoriaService = categoriaService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaResponse>> CadastroDeCategoria(CategoriaRequest request)
    {
        CategoriaResponse response = await _categoriaService.CreateCategoria(request);
        return CreatedAtAction(nameof(BuscarCategoriaPorId),new 
            { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoriaResponse>> AlterarCategoria(int id, CategoriaRequest request)
    {
        return Ok(await _categoriaService.UpdateCategoria(id, request));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<CategoriaResponse>> DeletarCategoria(int id)
    { 
      return Ok(await _categoriaService.DeleteCategoria(id));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponse>> BuscarCategoriaPorId(int id)
    {
        return Ok(await _categoriaService.GetCategoriaById(id));
    }
    
    [HttpGet("Filter/Pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaResponse>>> ListarCategoriaComFiltro([FromQuery] CategoriaFiltroRequest filtroRequest)
    {
        IPagedList<Categoria> categorias = await _categoriaService.GetAllFilterCategorias(filtroRequest);
        Response.Headers.Append("X-Pagination", 
            JsonConvert.SerializeObject(MetaData<Categoria>.ToValue(categorias)));
        return Ok(_mapper.Map<IEnumerable<CategoriaResponse>>(categorias));
    }
    
}