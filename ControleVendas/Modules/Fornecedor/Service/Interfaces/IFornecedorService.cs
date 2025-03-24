using ControleVendas.Modules.Fornecedor.Models.Request;
using ControleVendas.Modules.Fornecedor.Models.Response;

namespace ControleVendas.Modules.Fornecedor.Service.Interfaces;

public interface IFornecedorService
{
    Task<FornecedorResponse> CreateFornecedor(FornecedorRequest request);
    
    Task<FornecedorResponse> GetFornecedorById(int id); 
    
    Task<FornecedorPaginationResponse> GetAllFilterFornecedor(FornecedorFiltroRequest filtroRequest);
    
    Task UpdateFornecedor(int id, FornecedorRequest request);
    
    Task DeleteFornecedor(int id);
}