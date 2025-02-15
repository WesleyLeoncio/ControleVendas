using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Models.Response;

namespace ControleVendas.Modules.Produto.Service.Interfaces;

public interface IProdutoService
{
    Task<ProdutoResponse> CreateProduto(ProdutoRequest request);
    Task<ProdutoResponse> GetProdutoById(int id); 
    Task<ProdutoPaginationResponse> GetAllFilterProdutos(ProdutoFiltroRequest filtroRequest);
    
    Task<ProdutoResponse> UpdateProduto(int id, ProdutoRequest request);
    
    Task<ProdutoResponse> DeleteProduto(int id);
    
}