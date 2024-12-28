using controle_vendas.modules.categoria.model.request;
using controle_vendas.modules.categoria.model.response;
using controle_vendas.modules.categoria.service.interfaces;
using Microsoft.AspNetCore.Mvc;

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
        return CreatedAtAction(nameof(BuscarCategoriaPorId),new 
            { id = response.Id }, response);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponse>> BuscarCategoriaPorId(int id)
    {
        return Ok(await _categoriaService.GetCategoriaById(id));
    }
}