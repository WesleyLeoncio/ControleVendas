using ControleVendas.Modules.Cliente.Models.Response;
using ControleVendas.Modules.Pedido.Models.Enums;

namespace ControleVendas.Modules.Pedido.Models.Response;

public record PedidoResponse(
    int Id,
    ClienteResponse Cliente,
    MetodoPagamento FormaPagamento,
    int NumeroParcelas,
    decimal Desconto,
    StatusPedido Status,
    DateTime DataVenda, 
    decimal ValorPago,
    decimal ValorTotal
);