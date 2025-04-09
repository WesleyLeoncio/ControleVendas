using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Models.Response;

namespace ControleVendas.Modules.Pedido.Service.Interfaces;

public interface IPedidoService
{
    Task RegistrarPedido(PedidoRequest pedidoRequest);
    
    Task<PedidoPaginationResponse> GetAllFilterPedidos(PedidoFiltroRequest filtro);

    Task VerificarPedidosAtrasados();

    Task RealizarPagamentoDePedido(PedidoPagamentoRequest pagamentoRequest);
}