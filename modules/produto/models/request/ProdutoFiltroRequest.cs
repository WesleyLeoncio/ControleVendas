using System.ComponentModel.DataAnnotations;
using controle_vendas.modules.categoria.model;
using controle_vendas.modules.common.pagination.models.request;
using controle_vendas.modules.produto.models.enums;

namespace controle_vendas.modules.produto.models.request;

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