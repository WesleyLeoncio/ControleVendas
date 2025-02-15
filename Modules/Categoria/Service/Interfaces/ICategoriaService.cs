using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Categoria.Models.Response;

namespace ControleVendas.Modules.Categoria.Service.Interfaces;

public interface ICategoriaService
{
    Task<CategoriaResponse> CreateCategoria(CategoriaRequest request);
    
    Task<CategoriaResponse> GetCategoriaById(int id); 
    
    Task<CategoriaPaginationResponse> GetAllFilterCategorias(CategoriaFiltroRequest filtroRequest);
    
    Task<CategoriaPaginationProdutoResponse> GetAllIncludeProduto(CategoriaFiltroRequest filtroRequest);
    
    Task<CategoriaResponse> UpdateCategoria(int id, CategoriaRequest request);
    
    Task<CategoriaResponse> DeleteCategoria(int id);
    
}