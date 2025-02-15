using ControleVendas.Modules.Produto.Models.Request;

namespace ControleVendas.Modules.Produto.Repository.Filter.Interfaces;

public interface IFilterProdutoResult
{
    public IQueryable<Models.Entity.Produto> RunFilter(IQueryable<Models.Entity.Produto> queryable, ProdutoFiltroRequest filtro);
}