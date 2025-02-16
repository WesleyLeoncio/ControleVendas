using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Common.Repository.Interfaces;
using X.PagedList;

namespace ControleVendas.Modules.Categoria.Repository.Interfaces;

public interface ICategoriaRepository : IRepository<CategoriaEntity>
{
    Task<IPagedList<CategoriaEntity>> GetAllIncludePageableAsync(CategoriaFiltroRequest filtroRequest);
    
    Task<IPagedList<CategoriaEntity>> GetAllFilterPageableAsync(CategoriaFiltroRequest filtroRequest);
}