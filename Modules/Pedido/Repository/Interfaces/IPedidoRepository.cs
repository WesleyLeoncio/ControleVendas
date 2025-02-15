using ControleVendas.Modules.Common.Repository.Interfaces;
using ControleVendas.Modules.Pedido.Models.Request;
using X.PagedList;

namespace ControleVendas.Modules.Pedido.Repository.Interfaces;

public interface IPedidoRepository : IRepository<Models.Entity.Pedido>
{
    Task<IPagedList<Models.Entity.Pedido>> GetAllIncludeClienteFilterPageableAsync(PedidoFiltroRequest filtroRequest);

    Task<IEnumerable<Models.Entity.Pedido>> GetAllPedidosStatusPendente();
}