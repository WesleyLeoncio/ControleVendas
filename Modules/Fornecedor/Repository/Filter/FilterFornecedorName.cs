using ControleVendas.Modules.Fornecedor.Models.Entity;

namespace ControleVendas.Modules.Fornecedor.Repository.Filter;

public abstract class FilterFornecedorName
{
    public static IQueryable<FornecedorEntity> RunFilterName(IQueryable<FornecedorEntity> queryable, string? name)
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