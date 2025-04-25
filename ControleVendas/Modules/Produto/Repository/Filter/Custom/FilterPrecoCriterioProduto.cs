using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Enums;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Produto.Repository.Filter.Custom;

public class FilterPrecoCriterioProduto : IFilterProdutoResult
{
    public IQueryable<ProdutoEntity> RunFilter(IQueryable<ProdutoEntity> queryable, ProdutoFiltroRequest filtro)
    {
        if (filtro.VerificarValores())
        {
            switch (filtro.PrecoCriterio)
            {
                case Criterio.Maior:
                    queryable = queryable.Where(p => p.ValorVenda > filtro.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
                case Criterio.Menor:
                    queryable = queryable.Where(p => p.ValorVenda < filtro.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
                case Criterio.Igual:
                    queryable = queryable.Where(p => p.ValorVenda == filtro.Preco)
                        .OrderBy(p => p.ValorVenda);
                    break;
            }

            return queryable;
        }
        return queryable;
    }
}