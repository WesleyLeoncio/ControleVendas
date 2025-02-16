using ControleVendas.Infra.Data;
using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Cliente.Repository.Filter;
using ControleVendas.Modules.Cliente.Repository.Interfaces;
using ControleVendas.Modules.Common.Repository;
using X.PagedList;
using X.PagedList.Extensions;

namespace ControleVendas.Modules.Cliente.Repository;

public class ClienteRepository : Repository<ClienteEntity>, IClienteRepository
{
    public ClienteRepository(AppDbConnectionContext context) : base(context)
    {
    }

    public Task<IPagedList<ClienteEntity>> GetAllFilterPageableAsync(ClienteFiltroRequest filtroRequest)
    {
        IQueryable<ClienteEntity> clienteQuery = GetIQueryable();
        
        clienteQuery = FilterClienteName.RunFilterName(clienteQuery, filtroRequest.Nome);
        
        return Task.FromResult(clienteQuery.ToPagedList(filtroRequest.PageNumber, filtroRequest.PageSize));

    }
}