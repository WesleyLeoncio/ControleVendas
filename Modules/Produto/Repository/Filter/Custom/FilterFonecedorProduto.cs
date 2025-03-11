using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Produto.Repository.Filter.Custom;

public class FilterFonecedorProduto : IFilterProdutoResult
{
    public IQueryable<ProdutoEntity> RunFilter(IQueryable<ProdutoEntity> queryable, ProdutoFiltroRequest filtro)
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