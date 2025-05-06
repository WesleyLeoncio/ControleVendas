using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Cliente.Models.Response;

namespace ControleVendas.Modules.Cliente.Service.Interfaces;

public interface IClienteService
{
    Task<ClienteResponse> CreateCliente(ClienteRequest request);
    
    Task<ClienteResponse> GetClienteById(int id); 
    Task<ClientePaginationResponse> GetAllFilterClientes(ClienteFiltroRequest filtroRequest);
    
    Task UpdateCliente(int id, ClienteRequest request);
    
    Task DeleteCliente(int id);
    
    Task AlterStatusCliente(int id);
}