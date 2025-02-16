using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Pedido.Repository.Filter.Custom;

public class FilterNameClientePedido : IFilterPedidoResult
{
    public IQueryable<PedidoEntity> RunFilter(IQueryable<PedidoEntity> queryable, PedidoFiltroRequest filtro)
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