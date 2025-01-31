using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.models.response;

namespace controle_vendas.modules.produto.service.interfaces;

public interface IProdutoService
{
    Task<ProdutoResponse> CreateCategoria(ProdutoRequest request);
    Task<ProdutoResponse> GetCategoriaById(int id); 
    Task<ProdutoPaginationResponse> GetAllFilterProdutos(ProdutoFiltroRequest filtroRequest);
    //
    // Task<CategoriaResponse> UpdateCategoria(int id, CategoriaRequest request);
    //
    // Task<CategoriaResponse> DeleteCategoria(int id); 
}