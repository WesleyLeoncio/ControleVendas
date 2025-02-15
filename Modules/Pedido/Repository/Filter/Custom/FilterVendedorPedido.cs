using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Pedido.Repository.Filter.Custom;

public class FilterVendedorPedido : IFilterPedidoResult
{
    public IQueryable<Models.Entity.Pedido> RunFilter(IQueryable<Models.Entity.Pedido> queryable, PedidoFiltroRequest filtro)
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