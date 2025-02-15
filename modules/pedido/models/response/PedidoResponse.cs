

using controle_vendas.modules.cliente.model.response;
using controle_vendas.modules.pedido.models.enums;

namespace controle_vendas.modules.pedido.models.response;

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