using System.ComponentModel.DataAnnotations;

namespace controle_vendas.modules.user.models.request;

public record UserRegisterRequest(
    [Required(ErrorMessage = "O campo UserName é Obrigatorio!")]
    string Username,
    [Required(ErrorMessage = "O campo Nome Completo é Obrigatorio!")]
    string FullName,
    [EmailAddress]
    [Required(ErrorMessage = "O campo Email é Obrigatorio!")]
    string Email,
    [Required(ErrorMessage = "O campo Password é Obrigatorio!")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "A senha deve conter pelo menos: uma letra maiúscula, uma letra minúscula, um número, um símbolo (ex: @, #, !, etc.) e ter no mínimo 8 caracteres.")]
    string Password,
    [Required(ErrorMessage = "Campo Telefone Obrigatorio!")]
    [RegularExpression(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$", ErrorMessage = "Número de telefone inválido.")]
    string PhoneNumber
);