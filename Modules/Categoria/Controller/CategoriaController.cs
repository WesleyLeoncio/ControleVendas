using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Categoria.Models.Response;
using ControleVendas.Modules.Categoria.Service.Interfaces;
using ControleVendas.Modules.User.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleVendas.Modules.Categoria.Controller;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = nameof(Role.VENDEDOR))]
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
    public async Task<ActionResult> AlterarCategoria(int id, CategoriaRequest request)
    {
        await _categoriaService.UpdateCategoria(id, request);
        return NoContent();
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
    public async Task<ActionResult<IEnumerable<CategoriaProdutoResponse>>> ListarCategoriaComProdutosFiltro([FromQuery] CategoriaFiltroRequest filtroRequest)
    {
        CategoriaPaginationProdutoResponse response = await _categoriaService.GetAllIncludeProduto(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Categorias);
    }
   
    
}