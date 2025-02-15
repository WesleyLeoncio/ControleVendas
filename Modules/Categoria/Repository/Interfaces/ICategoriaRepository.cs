using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Common.Repository.Interfaces;
using X.PagedList;

namespace ControleVendas.Modules.Categoria.Repository.Interfaces;

public interface ICategoriaRepository : IRepository<Models.Entity.Categoria>
{
    Task<IPagedList<Models.Entity.Categoria>> GetAllIncludePageableAsync(CategoriaFiltroRequest filtroRequest);
    
    Task<IPagedList<Models.Entity.Categoria>> GetAllFilterPageableAsync(CategoriaFiltroRequest filtroRequest);
}