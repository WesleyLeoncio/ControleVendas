using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Common.Pagination;

namespace ControleVendas.Modules.Cliente.Models.Response;

public class ClientePaginationResponse
{
    public IEnumerable<ClienteResponse> Clientes { get; set; }

    public MetaData<ClienteEntity> MetaData { get; set; }

    public ClientePaginationResponse(IEnumerable<ClienteResponse> clientes, MetaData<ClienteEntity> meta)
    {
        Clientes = clientes;
        MetaData = meta;
    }
}