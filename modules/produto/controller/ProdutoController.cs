using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.models.response;
using controle_vendas.modules.produto.service.interfaces;
using Microsoft.AspNetCore.Mvc;

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
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoResponse>> BuscarProdutoPorId(int id)
    {
        return Ok(await _produtoService.GetCategoriaById(id));
    }
}