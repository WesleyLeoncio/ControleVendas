using ControleVendas.Infra.Data;
using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Categoria.Repository.Filter;
using ControleVendas.Modules.Categoria.Repository.Interfaces;
using ControleVendas.Modules.Common.Repository;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace ControleVendas.Modules.Categoria.Repository;

public class CategoriaRepository : Repository<Models.Entity.Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbConnectionContext context) : base(context)
    {
    }

    public Task<IPagedList<Models.Entity.Categoria>> GetAllIncludePageableAsync(CategoriaFiltroRequest filtroRequest)
    {
        IQueryable<Models.Entity.Categoria> categoriaQuery = GetIQueryable()
            .OrderBy(c => c.Nome)
            .Include(categoria => categoria.Produtos);

        categoriaQuery = FilterCategoriaName.RunFilterName(categoriaQuery, filtroRequest.Nome);

        return Task.FromResult(categoriaQuery.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize));
    }

    public Task<IPagedList<Models.Entity.Categoria>> GetAllFilterPageableAsync(CategoriaFiltroRequest filtroRequest)
    {
        IQueryable<Models.Entity.Categoria> categoriaQuery = GetIQueryable();

        categoriaQuery = FilterCategoriaName.RunFilterName(categoriaQuery, filtroRequest.Nome);

        return Task.FromResult(categoriaQuery.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize));
    }
}