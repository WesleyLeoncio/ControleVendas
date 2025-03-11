using ControleVendas.Modules.Cliente.Models.Entity;

namespace ControleVendas.Modules.Cliente.Repository.Filter;

public abstract class FilterClienteName
{
    public static IQueryable<ClienteEntity> RunFilterName(IQueryable<ClienteEntity> queryable, string? name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            queryable = queryable.Where(q =>
                q.Nome != null && q.Nome.Contains(name));
            return queryable;
        }

        return queryable;
    }
}