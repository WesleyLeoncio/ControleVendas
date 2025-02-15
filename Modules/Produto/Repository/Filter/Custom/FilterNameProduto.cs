using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Produto.Repository.Filter.Custom;

public class FilterNameProduto : IFilterProdutoResult
{
    public IQueryable<Models.Entity.Produto> RunFilter(IQueryable<Models.Entity.Produto> queryable, ProdutoFiltroRequest filtro)
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