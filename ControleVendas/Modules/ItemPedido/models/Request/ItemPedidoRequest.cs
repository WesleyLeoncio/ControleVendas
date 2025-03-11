using System.ComponentModel.DataAnnotations;

namespace ControleVendas.Modules.ItemPedido.models.Request;

public record ItemPedidoRequest(
    [Required(ErrorMessage = "O ID do produto é obrigatório.")]
    int ProdutoId,
    [Required(ErrorMessage = "A quantidade do produto é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser no mínimo 1.")]
    int Quantidade
);