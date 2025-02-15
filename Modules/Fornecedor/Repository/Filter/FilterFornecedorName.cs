namespace ControleVendas.Modules.Fornecedor.Repository.Filter;

public class FilterFornecedorName
{
    public static IQueryable<Models.Entity.Fornecedor> RunFilterName(IQueryable<Models.Entity.Fornecedor> queryable, string? name)
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