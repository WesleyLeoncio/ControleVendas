using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Pedido.Repository.Filter.Custom;

public class FilterStatusPedido : IFilterPedidoResult
{
    public IQueryable<PedidoEntity> RunFilter(IQueryable<PedidoEntity> queryable, PedidoFiltroRequest filtro)
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