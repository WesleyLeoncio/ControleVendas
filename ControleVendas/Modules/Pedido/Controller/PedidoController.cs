﻿using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Models.Response;
using ControleVendas.Modules.Pedido.Service.Interfaces;
using ControleVendas.Modules.User.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleVendas.Modules.Pedido.Controller;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = nameof(Role.Vendedor))]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidoController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }
    
    ///<summary>Cadastra Um Novo Pedido</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
    [HttpPost]
    public async Task<ActionResult> CriarNovoPedido(PedidoRequest request)
    {
        await _pedidoService.RegistrarPedido(request);
        return StatusCode(201);
    }
    
    ///<summary>Registrar Um Pagamento Para Um Pedido</summary>
    [HttpPost("Pagamento")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> RealizarPagamento(PedidoPagamentoRequest request)
    {
        await _pedidoService.RealizarPagamentoDePedido(request);
        return NoContent();
    }
    
    ///<summary>Cancela um pedido</summary>
    [HttpPut("Cancelar/{pedidoId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> CancelaPeido(int pedidoId)
    {
        await _pedidoService.CancelarPedido(pedidoId);
        return NoContent();
    }
    
    ///<summary>Lista Pedidos Utilizando Filtros</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [HttpGet]
    public async Task<ActionResult<PedidoPaginationResponse>> ListarPedidosComFiltros([FromQuery] PedidoFiltroRequest filtroRequest)
    {
        PedidoPaginationResponse response = await _pedidoService.GetAllFilterPedidos(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Pedidos);
    }

    
}