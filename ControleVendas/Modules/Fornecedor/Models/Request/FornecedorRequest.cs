using System.ComponentModel.DataAnnotations;

namespace ControleVendas.Modules.Fornecedor.Models.Request;

public record FornecedorRequest(
    [Required(ErrorMessage = "Campo Nome Obrigatório!")]
    [StringLength(80, ErrorMessage = "O campo Nome deve ter no máximo 80 caracteres!")]
    string Nome
);