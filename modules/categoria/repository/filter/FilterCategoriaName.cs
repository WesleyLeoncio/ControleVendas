using controle_vendas.modules.categoria.model.entity;

namespace controle_vendas.modules.categoria.repository.filter;

public abstract class FilterCategoriaName
{
    public static IQueryable<Categoria> RunFilterName(IQueryable<Categoria> queryable, string? name)
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