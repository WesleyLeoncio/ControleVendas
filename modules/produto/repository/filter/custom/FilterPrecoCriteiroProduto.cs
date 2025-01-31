using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.enums;
using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.repository.filter.interfaces;

namespace controle_vendas.modules.produto.repository.filter.custom;

public class FilterPrecoCriteiroProduto : IFilterProdutoResult
{
    public IQueryable<Produto> RunFilter(IQueryable<Produto> queryable, ProdutoFiltroRequest filtro)
    {
        if (filtro.VerificarValores())
        {
            switch (filtro.PrecoCriterio)
            {
                case Criterio.MAIOR:
                    queryable = queryable.Where(p => p.ValorVenda > filtro.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
                case Criterio.MENOR:
                    queryable = queryable.Where(p => p.ValorVenda < filtro.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
                case Criterio.IGUAL:
                    queryable = queryable.Where(p => p.ValorVenda == filtro.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
            }

            return queryable;
        }
        return queryable;
    }
}