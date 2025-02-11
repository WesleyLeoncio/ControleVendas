using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.repository.filter.interfaces;

namespace controle_vendas.modules.produto.repository.filter.custom;

public class FilterProdutoEstoque : IFilterProdutoResult
{
    public IQueryable<Produto> RunFilter(IQueryable<Produto> queryable, ProdutoFiltroRequest filtro)
    {
         return queryable.Where(q => q.Estoque > 0);
    }
}