using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Models.Response;
using ControleVendas.Modules.Produto.Service.Interfaces;
using ControleVendas.Modules.User.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleVendas.Modules.Produto.Controller;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = nameof(Role.Vendedor))]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }
    
    ///<summary>Cadastra Um Novo Produto</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
    [HttpPost]
    public async Task<ActionResult<ProdutoResponse>> CadastroDeProduto(ProdutoRequest request)
    {
        ProdutoResponse response = await _produtoService.CreateProduto(request);
        return CreatedAtAction(nameof(BuscarProdutoPorId),
            new { id = response.Id }, response);
    }
    
    /// <summary>Altera Um Produto</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [HttpPut("{id}")]
    public async Task<ActionResult> AlterarProduto(int id, ProdutoRequest request)
    {
        await _produtoService.UpdateProduto(id, request);
        return NoContent();
    }
    
    /// <summary>Delete Um Produto</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProdutoResponse>> DeleteProduto(int id)
    {
        return Ok(await _produtoService.DeleteProduto(id));
    }
    
    ///<summary>Lista Produtos Utilizando Filtros</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [HttpGet("Filter/Pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoResponse>>> ListarProdutosComFiltro([FromQuery] ProdutoFiltroRequest filtroRequest)
    {
        ProdutoPaginationResponse response = await _produtoService.GetAllFilterProdutos(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Produtos);
    }
    
    ///<summary>Busca Um Produto Pelo Id</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoResponse>> BuscarProdutoPorId(int id)
    {
        return Ok(await _produtoService.GetProdutoById(id));
    }
}