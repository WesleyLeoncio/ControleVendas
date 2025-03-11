using ControleVendas.Modules.Common.Pagination.Models.Request;

namespace ControleVendas.Modules.Cliente.Models.Request;

public class ClienteFiltroRequest : QueryParameters
{
    public string? Nome { get; set; }
}