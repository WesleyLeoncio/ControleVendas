using controle_vendas.modules.common.pagination.models.request;

namespace controle_vendas.modules.cliente.model.request;

public class ClienteFiltroRequest : QueryParameters
{
    public string? Nome { get; set; }
}