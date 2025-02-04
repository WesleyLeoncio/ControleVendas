using controle_vendas.infra.data;
using controle_vendas.modules.cliente.model.entity;
using controle_vendas.modules.cliente.model.request;
using controle_vendas.modules.cliente.repository.filter;
using controle_vendas.modules.cliente.repository.interfaces;
using controle_vendas.modules.common.repository;
using X.PagedList;
using X.PagedList.Extensions;

namespace controle_vendas.modules.cliente.repository;

public class ClienteRepository : Repository<Cliente>, IClienteRepository
{
    public ClienteRepository(AppDbConnectionContext context) : base(context)
    {
    }

    public Task<IPagedList<Cliente>> GetAllFilterPageableAsync(ClienteFiltroRequest filtroRequest)
    {
        IQueryable<Cliente> clienteQuery = GetIQueryable();
        
        clienteQuery = FilterClienteName.RunFilterName(clienteQuery, filtroRequest.Nome);
        
        return Task.FromResult(clienteQuery.ToPagedList(filtroRequest.PageNumber, filtroRequest.PageSize));

    }
}