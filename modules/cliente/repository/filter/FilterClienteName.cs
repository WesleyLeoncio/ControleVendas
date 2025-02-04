using controle_vendas.modules.cliente.model.entity;

namespace controle_vendas.modules.cliente.repository.filter;

public abstract class FilterClienteName
{
    public static IQueryable<Cliente> RunFilterName(IQueryable<Cliente> queryable, string? name)
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