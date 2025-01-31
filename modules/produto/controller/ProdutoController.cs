using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.models.response;
using controle_vendas.modules.produto.service.interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace controle_vendas.modules.produto.controller;

[ApiController]
[Route("[controller]")]
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
        ProdutoResponse response = await _produtoService.CreateCategoria(request);
        return CreatedAtAction(nameof(BuscarProdutoPorId),
            new { id = response.Id }, response);
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
        return Ok(await _produtoService.GetCategoriaById(id));
    }
}