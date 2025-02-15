using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.pedido.repository.filter.interfaces;

namespace controle_vendas.modules.pedido.repository.filter.custom;

public class FilterVendedorPedido : IFilterPedidoResult
{
    public IQueryable<Pedido> RunFilter(IQueryable<Pedido> queryable, PedidoFiltroRequest filtro)
    {
        if (!string.IsNullOrEmpty(filtro.VerdedorId))
        {
            queryable = queryable
                .Where(p => p.VendedorId == filtro.VerdedorId);
            return queryable;
            
        }

        return queryable;
    }
}