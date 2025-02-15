using controle_vendas.modules.common.repository.interfaces;
using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.models.request;
using X.PagedList;

namespace controle_vendas.modules.pedido.repository.interfaces;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<IPagedList<Pedido>> GetAllIncludeClienteFilterPageableAsync(PedidoFiltroRequest filtroRequest);

    Task<IEnumerable<Pedido>> GetAllPedidosStatusPendente();
}