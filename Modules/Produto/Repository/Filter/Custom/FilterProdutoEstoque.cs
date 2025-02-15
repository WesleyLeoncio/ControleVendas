using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Repository.Filter.Interfaces;

namespace ControleVendas.Modules.Produto.Repository.Filter.Custom;

public class FilterProdutoEstoque : IFilterProdutoResult
{
    public IQueryable<Models.Entity.Produto> RunFilter(IQueryable<Models.Entity.Produto> queryable, ProdutoFiltroRequest filtro)
    {
         return queryable.Where(q => q.Estoque > 0);
    }
}