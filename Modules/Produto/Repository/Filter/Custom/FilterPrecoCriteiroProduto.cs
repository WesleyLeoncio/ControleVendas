using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Enums;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Produto.Repository.Filter.Custom;

public class FilterPrecoCriteiroProduto : IFilterProdutoResult
{
    public IQueryable<ProdutoEntity> RunFilter(IQueryable<ProdutoEntity> queryable, ProdutoFiltroRequest filtro)
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