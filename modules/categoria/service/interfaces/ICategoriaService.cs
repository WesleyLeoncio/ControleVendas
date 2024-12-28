using controle_vendas.modules.categoria.model.request;
using controle_vendas.modules.categoria.model.response;

namespace controle_vendas.modules.categoria.service.interfaces;

public interface ICategoriaService
{
    Task<CategoriaResponse> CreateCategoria(CategoriaRequest categoria);
    
    Task<CategoriaResponse> GetCategoriaById(int id); 
    
    Task<CategoriaResponse> GetAllFilterCategorias(CategoriaFiltroRequest filtroRequest);
    
    Task<CategoriaResponse> UpdateCategoria(int id, CategoriaRequest categoria);
    
    Task<CategoriaResponse> DeleteCategoria(int id);
}