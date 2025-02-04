using controle_vendas.modules.cliente.model.entity;
using controle_vendas.modules.common.pagination;

namespace controle_vendas.modules.cliente.model.response;

public class ClientePaginationResponse
{
    public IEnumerable<ClienteResponse> Clientes { get; set; }
    
    public MetaData<Cliente> MetaData { get; set; }

    public ClientePaginationResponse(IEnumerable<ClienteResponse> clientes, MetaData<Cliente> meta)
    {
        Clientes = clientes;
        MetaData = meta;
    }
}