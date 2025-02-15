using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.pedido.models.response;
using controle_vendas.modules.pedido.service.interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace controle_vendas.modules.pedido.controller;

[ApiController]
[Route("[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public async Task<ActionResult> CriarNovoPedido(PedidoRequest request)
    {
        await _pedidoService.RegistrarPedido(request);
        return StatusCode(201);
    }

    [HttpGet]
    public async Task<ActionResult<PedidoPaginationResponse>> ListarPedidosComFiltros([FromQuery] PedidoFiltroRequest filtroRequest)
    {
        PedidoPaginationResponse response = await _pedidoService.GetAllFilterPedidos(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Pedidos);
    }


    [HttpGet("Teste")]
    public async Task Teste()
    {
        await _pedidoService.VerificarPedidosAtrasados();
    }
}