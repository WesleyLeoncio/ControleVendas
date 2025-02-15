using ControleVendas.Modules.Pedido.Models.Request;

namespace ControleVendas.Modules.Pedido.Repository.Filter.Interfaces;

public interface IFilterPedidoResult
{
    public IQueryable<Models.Entity.Pedido> RunFilter(IQueryable<Models.Entity.Pedido> queryable, PedidoFiltroRequest filtro);
}