using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Common.Repository.Interfaces;
using X.PagedList;

namespace ControleVendas.Modules.Cliente.Repository.Interfaces;

public interface IClienteRepository : IRepository<Models.Entity.Cliente>
{
    Task<IPagedList<Models.Entity.Cliente>> GetAllFilterPageableAsync(ClienteFiltroRequest filtroRequest);
}