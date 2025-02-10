using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.models.request;


namespace controle_vendas.modules.pedido.service.interfaces;

public interface IPedidoService
{
    Task<Pedido> CreatePedido(PedidoRequest request);
}