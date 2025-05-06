using ControleVendas.Infra.Data;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.Repository;
using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Enums;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Repository.Filter.Custom;
using ControleVendas.Modules.Pedido.Repository.Filter.Interfaces;
using ControleVendas.Modules.Pedido.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace ControleVendas.Modules.Pedido.Repository;

public class PedidoRepository : Repository<PedidoEntity>, IPedidoRepository
{
    public PedidoRepository(AppDbConnectionContext context) : base(context)
    {
    }

   
    public Task<IPagedList<PedidoEntity>> GetAllIncludeClienteFilterPageableAsync(PedidoFiltroRequest filtroRequest)
    {
        IQueryable<PedidoEntity> pedidoQuery = GetIQueryable();
        
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
    
    public async Task<IEnumerable<PedidoEntity>> GetAllPedidosStatusPendente()
    {
        IQueryable<PedidoEntity> pedidoQuery = GetIQueryable();
        
        pedidoQuery = pedidoQuery.Where(p => p.Status == StatusPedido.Pendente);
        
        return await pedidoQuery.ToListAsync();
    }

    public async Task<PedidoEntity> GetPedidosIncludeItensPendentePorId(int id)
    {
        var pedido = await GetIQueryable()
            .Include(p => p.Itens)
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (pedido == null)
            throw new NotFoundException("Pedido não encontrado!");

        return pedido;
    }
}