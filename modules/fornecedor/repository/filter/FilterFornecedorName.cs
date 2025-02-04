using controle_vendas.modules.fornecedor.model.entity;

namespace controle_vendas.modules.fornecedor.repository.filter;

public class FilterFornecedorName
{
    public static IQueryable<Fornecedor> RunFilterName(IQueryable<Fornecedor> queryable, string? name)
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