using System.ComponentModel.DataAnnotations;
using controle_vendas.modules.item_pedido.models.request;
using controle_vendas.modules.pedido.models.enums;

namespace controle_vendas.modules.pedido.models.request;

public class PedidoRequest
{
    [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
    public int ClienteId { get; set; }
    
    [Required(ErrorMessage = "A forma de pagamento é obrigatória.")]
    [EnumDataType(typeof(MetodoPagamento))]
    public MetodoPagamento FormaPagamento { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "O número de parcelas deve ser pelo menos 1.")]
    public int NumeroParcelas { get; set; } = 1;

    [Range(0, double.MaxValue, ErrorMessage = "O desconto não pode ser negativo.")]
    public decimal Desconto { get; set; } = 0;

    [Required(ErrorMessage = "Os itens do pedido são obrigatórios.")]
    [MinLength(1, ErrorMessage = "O pedido deve ter pelo menos um item.")]
    public List<ItemPedidoRequest> Itens { get; set; } = new List<ItemPedidoRequest>();
}