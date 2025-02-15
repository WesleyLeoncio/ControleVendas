using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.models.request;

namespace controle_vendas.modules.pedido.repository.filter.interfaces;

public interface IFilterPedidoResult
{
    public IQueryable<Pedido> RunFilter(IQueryable<Pedido> queryable, PedidoFiltroRequest filtro);
}