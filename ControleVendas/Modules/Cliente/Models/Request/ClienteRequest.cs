using System.ComponentModel.DataAnnotations;

namespace ControleVendas.Modules.Cliente.Models.Request;

public record ClienteRequest(
    [Required(ErrorMessage = "Campo Nome Obrigatório!")]
    [StringLength(180, ErrorMessage = "O campo Nome deve ter no máximo 80 caracteres!")]
    string Nome,
    
    [EmailAddress(ErrorMessage = "Formato de email incorreto!")]
    string Email,
    
    [Required(ErrorMessage = "Campo Telefone Obrigatório!")]
    [RegularExpression(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$", ErrorMessage = "Número de telefone inválido.")]
    string Telefone
    
);