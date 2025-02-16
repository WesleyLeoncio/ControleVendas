using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Models.Response;
using ControleVendas.Modules.Pedido.Service.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleVendas.Modules.Pedido.Controller;

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
    
    [HttpPost("Pagamento")]
    public async Task<ActionResult> RealizarPagamento(PedidoPagamentoRequest request)
    {
        await _pedidoService.PedidoPagamento(request);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<PedidoPaginationResponse>> ListarPedidosComFiltros([FromQuery] PedidoFiltroRequest filtroRequest)
    {
        PedidoPaginationResponse response = await _pedidoService.GetAllFilterPedidos(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Pedidos);
    }

    
}