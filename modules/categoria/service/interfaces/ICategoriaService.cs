using controle_vendas.modules.categoria.model.request;
using controle_vendas.modules.categoria.model.response;

namespace controle_vendas.modules.categoria.service.interfaces;

public interface ICategoriaService
{
    Task<CategoriaResponse> CreateCategoria(CategoriaRequest request);
    
    Task<CategoriaResponse> GetCategoriaById(int id); 
    
    Task<CategoriaResponse> GetAllFilterCategorias(CategoriaFiltroRequest filtroRequest);
    
    Task<CategoriaResponse> UpdateCategoria(int id, CategoriaRequest request);
    
    Task<CategoriaResponse> DeleteCategoria(int id);
}