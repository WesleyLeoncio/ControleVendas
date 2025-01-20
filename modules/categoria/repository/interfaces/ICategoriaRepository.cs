using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.categoria.model.request;
using controle_vendas.modules.common.pagination.models.request;
using controle_vendas.modules.common.repository.interfaces;
using X.PagedList;

namespace controle_vendas.modules.categoria.repository.interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<IPagedList<Categoria>> GetAllIncludePageableAsync(CategoriaFiltroRequest filtroRequest);
    
    Task<IPagedList<Categoria>> GetAllFilterPageableAsync(CategoriaFiltroRequest filtroRequest);
}