using controle_vendas.infra.data;
using controle_vendas.modules.common.repository;
using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.models.enums;
using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.pedido.repository.filter.custom;
using controle_vendas.modules.pedido.repository.filter.interfaces;
using controle_vendas.modules.pedido.repository.interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace controle_vendas.modules.pedido.repository;

public class PedidoRepository : Repository<Pedido>, IPedidoRepository
{
    public PedidoRepository(AppDbConnectionContext context) : base(context)
    {
    }

    public Task<IPagedList<Pedido>> GetAllIncludeClienteFilterPageableAsync(PedidoFiltroRequest filtroRequest)
    {
        IQueryable<Pedido> pedidoQuery = GetIQueryable();
        
        pedidoQuery = pedidoQuery.Include(p => p.Cliente);

        IEnumerable<IFilterPedidoResult> filterResults = new List<IFilterPedidoResult>
        {
            new FilterVendedorPedido(),
            new FilterNameClientePedido(),
            new FilterStatusPedido()
        };
        
        foreach (var filter in filterResults)
        {
            pedidoQuery = filter.RunFilter(pedidoQuery, filtroRequest);
        }
        
        return Task.FromResult(pedidoQuery.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize));
    }
    
    public async Task<IEnumerable<Pedido>> GetAllPedidosStatusPendente()
    {
        IQueryable<Pedido> pedidoQuery = GetIQueryable();
        
        pedidoQuery = pedidoQuery.Where(p => p.Status == StatusPedido.Pendente);
        
        return await pedidoQuery.ToListAsync();
    }
}