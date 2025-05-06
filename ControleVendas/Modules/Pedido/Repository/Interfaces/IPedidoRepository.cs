using ControleVendas.Modules.Common.Repository.Interfaces;
using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Request;
using X.PagedList;

namespace ControleVendas.Modules.Pedido.Repository.Interfaces;

public interface IPedidoRepository : IRepository<PedidoEntity>
{
    Task<IPagedList<PedidoEntity>> GetAllIncludeClienteFilterPageableAsync(PedidoFiltroRequest filtroRequest);

    Task<IEnumerable<PedidoEntity>> GetAllPedidosStatusPendente();
    
    Task<PedidoEntity> GetPedidosIncludeItensPendentePorId(int id);
}