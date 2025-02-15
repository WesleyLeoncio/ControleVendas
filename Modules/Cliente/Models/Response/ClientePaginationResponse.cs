using ControleVendas.Modules.Common.Pagination;

namespace ControleVendas.Modules.Cliente.Models.Response;

public class ClientePaginationResponse
{
    public IEnumerable<ClienteResponse> Clientes { get; set; }
    
    public MetaData<Entity.Cliente> MetaData { get; set; }

    public ClientePaginationResponse(IEnumerable<ClienteResponse> clientes, MetaData<Entity.Cliente> meta)
    {
        Clientes = clientes;
        MetaData = meta;
    }
}