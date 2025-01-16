using System.ComponentModel.DataAnnotations;

namespace controle_vendas.modules.fornecedor.model.request;

public record FornecedorRequest(
    [Required(ErrorMessage = "Campo Nome Obrigatorio!")]
    [StringLength(80, ErrorMessage = "O campo Nome deve ter no máximo 80 caracteres!")]
    string Nome
);