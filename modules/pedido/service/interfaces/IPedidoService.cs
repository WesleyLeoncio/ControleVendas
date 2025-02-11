using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.user.models.entity;

namespace controle_vendas.modules.pedido.service.interfaces;

public interface IPedidoService
{
    Task RegistrarPedido(PedidoRequest pedidoRequest);
}