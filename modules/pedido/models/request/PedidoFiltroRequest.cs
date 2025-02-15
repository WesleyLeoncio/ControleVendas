using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using controle_vendas.modules.common.pagination.models.request;
using controle_vendas.modules.pedido.models.enums;

namespace controle_vendas.modules.pedido.models.request;

public class PedidoFiltroRequest : QueryParameters
{
    public string? Nome { get; set; }
    [EnumDataType(typeof(StatusPedido))] 
    public StatusPedido? Status { get; set; }
    [JsonIgnore]
    public string? VerdedorId { get; set; }

    public bool VerificarStatusPedido()
    {
        return Status.HasValue;
    }
}
    
