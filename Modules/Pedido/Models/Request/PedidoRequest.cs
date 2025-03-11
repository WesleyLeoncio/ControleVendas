using System.ComponentModel.DataAnnotations;
using ControleVendas.Modules.ItemPedido.models.Request;
using ControleVendas.Modules.Pedido.Models.Enums;

namespace ControleVendas.Modules.Pedido.Models.Request;

public class PedidoRequest
{
    [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
    public int ClienteId { get; set; }
    
    [Required(ErrorMessage = "A forma de pagamento é obrigatória.")]
    [EnumDataType(typeof(MetodoPagamento))]
    public MetodoPagamento FormaPagamento { get; set; }

    [Required(ErrorMessage = "O numero de parcelas é obrigatória.")]
    public int NumeroParcelas { get; set; } = 0;

    [Range(0, double.MaxValue, ErrorMessage = "O desconto não pode ser negativo.")]
    public decimal Desconto { get; set; } = 0;
    
    [Range(0, double.MaxValue, ErrorMessage = "O pagamento não pode ser negativo.")]
    public decimal Pagamento { get; set; } = 0;

    [Required(ErrorMessage = "Os itens do pedido são obrigatórios.")]
    [MinLength(1, ErrorMessage = "O pedido deve ter pelo menos um item.")]
    public List<ItemPedidoRequest> Itens { get; set; } = new List<ItemPedidoRequest>();
}