using controle_vendas.modules.cliente.model.entity;
using controle_vendas.modules.cliente.model.request;
using controle_vendas.modules.common.repository.interfaces;
using X.PagedList;

namespace controle_vendas.modules.cliente.repository.interfaces;

public interface IClienteRepository : IRepository<Cliente>
{
    Task<IPagedList<Cliente>> GetAllFilterPageableAsync(ClienteFiltroRequest filtroRequest);
}