using ControleVendas.Modules.Common.Pagination.Models.Request;

namespace ControleVendas.Modules.Fornecedor.Models.Request;

public class FornecedorFiltroRequest : QueryParameters
{
    public string? Nome { get; set; }
}