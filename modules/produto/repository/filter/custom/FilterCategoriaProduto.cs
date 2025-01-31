using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.repository.filter.interfaces;

namespace controle_vendas.modules.produto.repository.filter.custom;

public class FilterCategoriaProduto : IFilterProdutoResult
{
    public IQueryable<Produto> RunFilter(IQueryable<Produto> queryable, ProdutoFiltroRequest filtro)
    {
        if (filtro.Categoria.HasValue)
        {
            queryable = queryable
                .Where(p => p.CategoriaId == filtro.Categoria);
            return queryable;
        }
        return queryable;
    }
}