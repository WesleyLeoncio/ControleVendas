using controle_vendas.modules.fornecedor.model.request;
using controle_vendas.modules.fornecedor.model.response;

namespace controle_vendas.modules.fornecedor.service.interfaces;

public interface IFornecedorService
{
    Task<FornecedorResponse> CreateFornecedor(FornecedorRequest request);
    
    Task<FornecedorResponse> GetFornecedorById(int id); 
    
    Task<FornecedorPaginationResponse> GetAllFilterFornecedor(FornecedorFiltroRequest filtroRequest);
    
    Task<FornecedorResponse> UpdateFornecedor(int id, FornecedorRequest request);
    
    Task<FornecedorResponse> DeleteFornecedor(int id);
}