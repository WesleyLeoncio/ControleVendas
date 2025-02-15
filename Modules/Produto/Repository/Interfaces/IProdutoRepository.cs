using ControleVendas.Modules.Common.Repository.Interfaces;
using ControleVendas.Modules.Produto.Models.Request;
using X.PagedList;

namespace ControleVendas.Modules.Produto.Repository.Interfaces;

public interface IProdutoRepository : IRepository<Models.Entity.Produto> 
{
    Task<IPagedList<Models.Entity.Produto>> GetAllFilterPageableAsync(ProdutoFiltroRequest filtroRequest); 
}