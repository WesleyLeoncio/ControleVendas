using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.pedido.repository.filter.interfaces;


namespace controle_vendas.modules.pedido.repository.filter.custom;

public class FilterNameClientePedido : IFilterPedidoResult
{
    public IQueryable<Pedido> RunFilter(IQueryable<Pedido> queryable, PedidoFiltroRequest filtro)
    {
        if (!string.IsNullOrEmpty(filtro.Nome))
        {
            queryable = queryable.Where(q =>
                q.Cliente != null && q.Cliente.Nome != null && q.Cliente.Nome.Contains(filtro.Nome));
            return queryable;
        }

        return queryable;
    }
}