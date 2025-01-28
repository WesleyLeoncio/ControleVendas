using controle_vendas.modules.categoria.model.request;
using controle_vendas.modules.categoria.model.response;
using controle_vendas.modules.categoria.service.interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace controle_vendas.modules.categoria.controller;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;
    
    public CategoriaController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaResponse>> CadastroDeCategoria(CategoriaRequest request)
    {
        CategoriaResponse response = await _categoriaService.CreateCategoria(request);
        return CreatedAtAction(nameof(BuscarCategoriaPorId),
            new { id = response.Id }, response);
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
        CategoriaPaginationResponse response = await _categoriaService.GetAllFilterCategorias(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Categorias);
    }
    
    [HttpGet("Filter/Pagination/Produtos")]
    public async Task<ActionResult<IEnumerable<CategoriaPaginationProdutoResponse>>> ListarCategoriaComProdutosFiltro([FromQuery] CategoriaFiltroRequest filtroRequest)
    {
        CategoriaPaginationProdutoResponse response = await _categoriaService.GetAllIncludeProduto(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Categorias);
    }
   
    
}