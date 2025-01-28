using controle_vendas.modules.common.repository.interfaces;
using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.request;
using X.PagedList;

namespace controle_vendas.modules.produto.repository.interfaces;

public interface IProdutoRepository : IRepository<Produto> 
{
    Task<IPagedList<Produto>> GetAllFilterPageableAsync(ProdutoFiltroRequest filtroRequest); 
}