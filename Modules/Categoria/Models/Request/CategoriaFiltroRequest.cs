using ControleVendas.Modules.Common.Pagination.Models.Request;

namespace ControleVendas.Modules.Categoria.Models.Request;

public class CategoriaFiltroRequest : QueryParameters
{
    public string? Nome { get; set; }
}