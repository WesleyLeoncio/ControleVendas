using ControleVendas.Infra.Data;
using ControleVendas.Modules.Common.Repository;
using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Repository.Filter.Custom;
using ControleVendas.Modules.Produto.Repository.Filter.Interfaces;
using ControleVendas.Modules.Produto.Repository.Interfaces;
using X.PagedList;
using X.PagedList.Extensions;

namespace ControleVendas.Modules.Produto.Repository;

public class ProdutoRepository : Repository<ProdutoEntity>, IProdutoRepository
{
    
    public ProdutoRepository(AppDbConnectionContext context) : base(context)
    {
    }

    public Task<IPagedList<ProdutoEntity>> GetAllFilterPageableAsync(ProdutoFiltroRequest filtroRequest)
    {
        IQueryable<ProdutoEntity> produtosQuery = GetIQueryable();
        
        IEnumerable<IFilterProdutoResult> filterResults = new List<IFilterProdutoResult>
        {
            new FilterProdutoEstoque(),
            new FilterNameProduto(),
            new FilterFonecedorProduto(),
            new FilterCategoriaProduto(),
            new FilterPrecoCriteiroProduto()
        };

        foreach (var filter in filterResults)
        {
            produtosQuery = filter.RunFilter(produtosQuery, filtroRequest);
        }
        

        return Task.FromResult(produtosQuery.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize));
    }
}