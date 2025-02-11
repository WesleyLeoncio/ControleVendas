using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.models.response;

namespace controle_vendas.modules.produto.service.interfaces;

public interface IProdutoService
{
    Task<ProdutoResponse> CreateProduto(ProdutoRequest request);
    Task<ProdutoResponse> GetProdutoById(int id); 
    Task<ProdutoPaginationResponse> GetAllFilterProdutos(ProdutoFiltroRequest filtroRequest);
    
    Task<ProdutoResponse> UpdateProduto(int id, ProdutoRequest request);
    
    Task<ProdutoResponse> DeleteProduto(int id);
    
}