using ControleVendas.Modules.Common.Repository.Interfaces;
using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using X.PagedList;

namespace ControleVendas.Modules.Produto.Repository.Interfaces;

public interface IProdutoRepository : IRepository<ProdutoEntity> 
{
    Task<IPagedList<ProdutoEntity>> GetAllFilterPageableAsync(ProdutoFiltroRequest filtroRequest); 
}