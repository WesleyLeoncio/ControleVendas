using controle_vendas.infra.data;
using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.categoria.model.request;
using controle_vendas.modules.categoria.repository.filter;
using controle_vendas.modules.categoria.repository.interfaces;
using controle_vendas.modules.common.repository;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace controle_vendas.modules.categoria.repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbConnectionContext context) : base(context)
    {
    }

    public Task<IPagedList<Categoria>> GetAllIncludePageableAsync(CategoriaFiltroRequest filtroRequest)
    {
        IQueryable<Categoria> categoriaQuery = GetIQueryable()
            .OrderBy(c => c.Nome)
            .Include(categoria => categoria.Produtos);

        categoriaQuery = FilterCategoriaName.RunFilterName(categoriaQuery, filtroRequest.Nome);

        return Task.FromResult(categoriaQuery.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize));
    }

    public Task<IPagedList<Categoria>> GetAllFilterPageableAsync(CategoriaFiltroRequest filtroRequest)
    {
        IQueryable<Categoria> categoriaQuery = GetIQueryable();

        categoriaQuery = FilterCategoriaName.RunFilterName(categoriaQuery, filtroRequest.Nome);

        return Task.FromResult(categoriaQuery.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize));
    }
}