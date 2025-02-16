using ControleVendas.Modules.Categoria.Models.Entity;

namespace ControleVendas.Modules.Categoria.Repository.Filter;

public abstract class FilterCategoriaName
{
    public static IQueryable<CategoriaEntity> RunFilterName(IQueryable<CategoriaEntity> queryable, string? name)
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