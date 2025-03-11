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

    ///<summary>Cadastra Uma Nova Categoria</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
    [HttpPost]
    
    public async Task<ActionResult<CategoriaResponse>> CadastroDeCategoria(CategoriaRequest request)
    {
        CategoriaResponse response = await _categoriaService.CreateCategoria(request);
        return CreatedAtAction(nameof(BuscarCategoriaPorId),
            new { id = response.Id }, response);
    }
    
    /// <summary>Altera Uma Categoria</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [HttpPut("{id}")]
    public async Task<ActionResult> AlterarCategoria(int id, CategoriaRequest request)
    {
        await _categoriaService.UpdateCategoria(id, request);
        return NoContent();
    }
    
    /// <summary>Deleta Uma Categoria</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CategoriaResponse>> DeletarCategoria(int id)
    { 
      return Ok(await _categoriaService.DeleteCategoria(id));
    }
    
    ///<summary>Busca Uma Categoria Pelo Id</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponse>> BuscarCategoriaPorId(int id)
    {
        return Ok(await _categoriaService.GetCategoriaById(id));
    }
    
    ///<summary>Lista Categorias Utilizando Filtros</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [HttpGet("Filter/Pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaResponse>>> ListarCategoriaComFiltro([FromQuery] CategoriaFiltroRequest filtroRequest)
    {
        CategoriaPaginationResponse response = await _categoriaService.GetAllFilterCategorias(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Categorias);
    }
    
    ///<summary>Lista Categorias Com Produtos Utilizando Filtros</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [HttpGet("Filter/Pagination/Produtos")]
    public async Task<ActionResult<IEnumerable<CategoriaProdutoResponse>>> ListarCategoriaComProdutosFiltro([FromQuery] CategoriaFiltroRequest filtroRequest)
    {
        CategoriaPaginationProdutoResponse response = await _categoriaService.GetAllIncludeProduto(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Categorias);
    }
   
    
}