using controle_vendas.infra.data;
using controle_vendas.modules.common.repository;
using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.repository.filter.custom;
using controle_vendas.modules.produto.repository.filter.interfaces;
using controle_vendas.modules.produto.repository.interfaces;
using X.PagedList;
using X.PagedList.Extensions;


namespace controle_vendas.modules.produto.repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    
    public ProdutoRepository(AppDbConnectionContext context) : base(context)
    {
    }

    public Task<IPagedList<Produto>> GetAllFilterPageableAsync(ProdutoFiltroRequest filtroRequest)
    {
        IQueryable<Produto> produtosQuery = GetIQueryable();
        
        IEnumerable<IFilterProdutoResult> filterResults = new List<IFilterProdutoResult>
        {
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