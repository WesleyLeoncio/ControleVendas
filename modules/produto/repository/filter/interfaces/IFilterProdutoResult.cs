using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.request;

namespace controle_vendas.modules.produto.repository.filter.interfaces;

public interface IFilterProdutoResult
{
    public IQueryable<Produto> RunFilter(IQueryable<Produto> queryable, ProdutoFiltroRequest filtro);
}