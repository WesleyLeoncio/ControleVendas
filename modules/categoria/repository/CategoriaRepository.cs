using controle_vendas.infra.data;
using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.categoria.model.request;
using controle_vendas.modules.categoria.repository.interfaces;
using controle_vendas.modules.common.pagination.models.request;
using controle_vendas.modules.common.repository;
using X.PagedList;
using X.PagedList.Extensions;

namespace controle_vendas.modules.categoria.repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbConnectionContext context) : base(context)
    {
    }

    public Task<IPagedList<Categoria>> GetAllIncludePageableAsync(QueryParameters queryParameters)
    {
        // IEnumerable<Categoria> categorias = await GetIQueryable()
        //     .OrderBy(c => c.Nome)
        //     .Include(categoria => categoria.Produtos)
        //     .ToListAsync();
        // return await categorias.ToPagedListAsync(queryParameters.PageNumber,
        //     queryParameters.PageSize);
        throw new NotImplementedException();
    }

    public async Task<IPagedList<Categoria>> GetAllFilterPageableAsync(CategoriaFiltroRequest filtroRequest)
    {
        IEnumerable<Categoria> categorias = await GetAllAsync();
        IQueryable<Categoria> queryableCategoria = 
            categorias.OrderBy(c => c.Nome).AsQueryable();
        
        if (!string.IsNullOrEmpty(filtroRequest.Nome))
        {
            queryableCategoria = queryableCategoria.Where(c =>
                c.Nome != null && c.Nome.Contains(filtroRequest.Nome));
        }
        return queryableCategoria.ToPagedList(filtroRequest.PageNumber,
            filtroRequest.PageSize);
        
    }
}