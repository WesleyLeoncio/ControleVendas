using controle_vendas.modules.common.pagination.models.request;

namespace controle_vendas.modules.fornecedor.model.request;

public class FornecedorFiltroRequest : QueryParameters
{
    public string? Nome { get; set; }
}