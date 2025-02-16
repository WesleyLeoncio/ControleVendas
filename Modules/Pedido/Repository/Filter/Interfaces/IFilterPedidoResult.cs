using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Request;

namespace ControleVendas.Modules.Pedido.Repository.Filter.Interfaces;

public interface IFilterPedidoResult
{
    public IQueryable<PedidoEntity> RunFilter(IQueryable<PedidoEntity> queryable, PedidoFiltroRequest filtro);
}