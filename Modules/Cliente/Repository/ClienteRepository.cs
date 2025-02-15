using ControleVendas.Infra.Data;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Cliente.Repository.Filter;
using ControleVendas.Modules.Cliente.Repository.Interfaces;
using ControleVendas.Modules.Common.Repository;
using X.PagedList;
using X.PagedList.Extensions;

namespace ControleVendas.Modules.Cliente.Repository;

public class ClienteRepository : Repository<Models.Entity.Cliente>, IClienteRepository
{
    public ClienteRepository(AppDbConnectionContext context) : base(context)
    {
    }

    public Task<IPagedList<Models.Entity.Cliente>> GetAllFilterPageableAsync(ClienteFiltroRequest filtroRequest)
    {
        IQueryable<Models.Entity.Cliente> clienteQuery = GetIQueryable();
        
        clienteQuery = FilterClienteName.RunFilterName(clienteQuery, filtroRequest.Nome);
        
        return Task.FromResult(clienteQuery.ToPagedList(filtroRequest.PageNumber, filtroRequest.PageSize));

    }
}