using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Common.Repository.Interfaces;
using X.PagedList;

namespace ControleVendas.Modules.Cliente.Repository.Interfaces;

public interface IClienteRepository : IRepository<ClienteEntity>
{
    Task<IPagedList<ClienteEntity>> GetAllFilterPageableAsync(ClienteFiltroRequest filtroRequest);
}