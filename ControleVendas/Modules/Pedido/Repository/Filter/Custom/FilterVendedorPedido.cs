using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Pedido.Repository.Filter.Custom;

public class FilterVendedorPedido : IFilterPedidoResult
{
    public IQueryable<PedidoEntity> RunFilter(IQueryable<PedidoEntity> queryable, PedidoFiltroRequest filtro)
    {
        if (!string.IsNullOrEmpty(filtro.VendedorId))
        {
            queryable = queryable
                .Where(p => p.VendedorId == filtro.VendedorId);
            return queryable;
            
        }

        return queryable;
    }
}