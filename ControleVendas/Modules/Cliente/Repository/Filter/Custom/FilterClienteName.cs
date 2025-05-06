using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Cliente.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Cliente.Repository.Filter.Custom;

public class FilterClienteName : IFilterClienteResult
{
    public IQueryable<ClienteEntity> RunFilter(IQueryable<ClienteEntity> queryable, ClienteFiltroRequest filtro)
    {
        if (!string.IsNullOrEmpty(filtro.Nome))
        {
            queryable = queryable.Where(q =>
                q.Nome != null && q.Nome.Contains(filtro.Nome));
            return queryable;
        }
        return queryable;
    }
}