using System.ComponentModel.DataAnnotations;
using ControleVendas.Modules.Common.Pagination.Models.Request;
using ControleVendas.Modules.Produto.Models.Enums;

namespace ControleVendas.Modules.Produto.Models.Request;

public class ProdutoFiltroRequest : QueryParameters
{
    public string? Nome { get; set; }
    
    public int? Fornecedor { get; set; }
    public int? Categoria { get; set; }
    public decimal? Preco { get; set; }
    
    
    [EnumDataType(typeof(Criterio))]
    public Criterio PrecoCriterio { get; set; }

    public bool VerificarValores()
    {
        return Preco.HasValue && Enum.IsDefined(PrecoCriterio);
    }
    
}