using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.pedido.service.interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public async Task Teste(PedidoRequest request)
    {
       await _pedidoService.RegistrarPedido(request);
    }
}