using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.repository.filter.interfaces;

namespace controle_vendas.modules.produto.repository.filter.custom;

public class FilterFonecedorProduto : IFilterProdutoResult
{
    public IQueryable<Produto> RunFilter(IQueryable<Produto> queryable, ProdutoFiltroRequest filtro)
    {
        if (filtro.Fornecedor.HasValue)
        {
            queryable = queryable
                .Where(p => p.FornecedorId == filtro.Fornecedor);
            return queryable;
            
        }

        return queryable;
    }
}