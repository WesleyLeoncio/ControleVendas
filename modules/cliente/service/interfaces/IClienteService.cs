using controle_vendas.modules.cliente.model.request;
using controle_vendas.modules.cliente.model.response;

namespace controle_vendas.modules.cliente.service.interfaces;

public interface IClienteService
{
    Task<ClienteResponse> CreateCliente(ClienteRequest request);
    
    Task<ClienteResponse> GetClienteById(int id); 
    Task<ClientePaginationResponse> GetAllFilterClientes(ClienteFiltroRequest filtroRequest);
    
    Task<ClienteResponse> UpdateCliente(int id, ClienteRequest request);
    
    Task<ClienteResponse> DeleteCliente(int id);
    
    Task<String> AlterStatusCliente(int id);
}