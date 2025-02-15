namespace ControleVendas.Modules.Categoria.Repository.Filter;

public abstract class FilterCategoriaName
{
    public static IQueryable<Models.Entity.Categoria> RunFilterName(IQueryable<Models.Entity.Categoria> queryable, string? name)
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