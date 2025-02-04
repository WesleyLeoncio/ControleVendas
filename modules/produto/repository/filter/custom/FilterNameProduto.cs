using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.repository.filter.interfaces;

namespace controle_vendas.modules.produto.repository.filter.custom;

public class FilterNameProduto : IFilterProdutoResult
{
    public IQueryable<Produto> RunFilter(IQueryable<Produto> queryable, ProdutoFiltroRequest filtro)
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