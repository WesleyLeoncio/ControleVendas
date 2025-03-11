using System.ComponentModel.DataAnnotations;

namespace ControleVendas.Modules.Pedido.Models.Request;

public record PedidoPagamentoRequest(
    [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
    int IdPedido,
    [Required(ErrorMessage = "O pagamento do pedido é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O pagamento não pode ser negativo.")]
    decimal Pagamento
);