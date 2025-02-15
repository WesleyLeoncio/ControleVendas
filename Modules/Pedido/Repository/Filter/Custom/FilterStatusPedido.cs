using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Pedido.Repository.Filter.Custom;

public class FilterStatusPedido : IFilterPedidoResult
{
    public IQueryable<Models.Entity.Pedido> RunFilter(IQueryable<Models.Entity.Pedido> queryable, PedidoFiltroRequest filtro)
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