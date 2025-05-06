using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;

namespace ControleVendas.Modules.Cliente.Repository.Filter.Interfaces;

public interface IFilterClienteResult
{
    public IQueryable<ClienteEntity> RunFilter(IQueryable<ClienteEntity> queryable, ClienteFiltroRequest filtro);
}