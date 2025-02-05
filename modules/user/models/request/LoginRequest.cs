using System.ComponentModel.DataAnnotations;

namespace controle_vendas.modules.user.models.request;

public record LoginRequest(
    [EmailAddress]
    [Required(ErrorMessage = "O campo Email é Obrigatorio!")]
    string Email,
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage =
            "A senha deve conter pelo menos: uma letra maiúscula, uma letra minúscula, um número, um símbolo (ex: @, #, !, etc.) e ter no mínimo 8 caracteres.")]
    string Password
);