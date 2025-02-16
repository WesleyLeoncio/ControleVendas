using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;

namespace ControleVendas.Modules.Produto.Repository.Filter.Interfaces;

public interface IFilterProdutoResult
{
    public IQueryable<ProdutoEntity> RunFilter(IQueryable<ProdutoEntity> queryable, ProdutoFiltroRequest filtro);
}