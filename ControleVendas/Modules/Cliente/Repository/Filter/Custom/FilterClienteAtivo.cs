using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Cliente.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Cliente.Repository.Filter.Custom;

public class FilterClienteAtivo : IFilterClienteResult
{
    public IQueryable<ClienteEntity> RunFilter(IQueryable<ClienteEntity> queryable, ClienteFiltroRequest filtro)
    {
        if (filtro.Ativo != null)
        {
            queryable = queryable.Where(q => q.Ativo == filtro.Ativo);
            return queryable;
        }

        return queryable;
    }
}