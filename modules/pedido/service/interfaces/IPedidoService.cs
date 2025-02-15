using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.pedido.models.response;


namespace controle_vendas.modules.pedido.service.interfaces;

public interface IPedidoService
{
    Task RegistrarPedido(PedidoRequest pedidoRequest);
    
    Task<PedidoPaginationResponse> GetAllFilterPedidos(PedidoFiltroRequest filtro);

    Task VerificarPedidosAtrasados();
}