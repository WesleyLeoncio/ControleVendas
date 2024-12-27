using controle_vendas.modules.common.pagination.models.request;

namespace controle_vendas.modules.categoria.model.request;

public class CategoriaFiltroRequest : QueryParameters
{
    public string? Nome { get; set; }
}