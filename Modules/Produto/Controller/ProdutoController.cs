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
[Authorize(Roles = nameof(Role.VENDEDOR))]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoResponse>> CadastroDeProduto(ProdutoRequest request)
    {
        ProdutoResponse response = await _produtoService.CreateProduto(request);
        return CreatedAtAction(nameof(BuscarProdutoPorId),
            new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProdutoResponse>> AlterarProduto(int id, ProdutoRequest request)
    {
        return Ok(await _produtoService.UpdateProduto(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProdutoResponse>> DeletarProduto(int id)
    {
        return Ok(await _produtoService.DeleteProduto(id));
    }

    [HttpGet("Filter/Pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoResponse>>> ListarProdutosComFiltro([FromQuery] ProdutoFiltroRequest filtroRequest)
    {
        ProdutoPaginationResponse response = await _produtoService.GetAllFilterProdutos(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Produtos);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoResponse>> BuscarProdutoPorId(int id)
    {
        return Ok(await _produtoService.GetProdutoById(id));
    }
}