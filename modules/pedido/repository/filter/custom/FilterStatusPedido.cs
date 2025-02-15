using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.pedido.repository.filter.interfaces;

namespace controle_vendas.modules.pedido.repository.filter.custom;

public class FilterStatusPedido : IFilterPedidoResult
{
    public IQueryable<Pedido> RunFilter(IQueryable<Pedido> queryable, PedidoFiltroRequest filtro)
    {
        if (filtro.VerificarStatusPedido())
        {
            queryable = queryable
                .Where(p => p.Status.Equals(filtro.Status));
            return queryable;
        }
        return queryable;
    }
}