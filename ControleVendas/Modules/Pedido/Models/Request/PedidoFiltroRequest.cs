using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ControleVendas.Modules.Common.Pagination.Models.Request;
using ControleVendas.Modules.Pedido.Models.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace ControleVendas.Modules.Pedido.Models.Request;

public class PedidoFiltroRequest : QueryParameters
{
    public string? Nome { get; set; }
    [EnumDataType(typeof(StatusPedido))] 
    public StatusPedido? Status { get; set; }
    [JsonIgnore]
    [SwaggerIgnore]
    public string? VendedorId { get; set; }

    public bool VerificarStatusPedido()
    {
        return Status.HasValue;
    }
}
    
