using System.ComponentModel.DataAnnotations;

namespace ControleVendas.Modules.User.Models.Request;

public record LoginRequest(
    
    [Required(ErrorMessage = "O campo UserName é Obrigatorio!")]
    string UserName,
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage =
            "A senha deve conter pelo menos: uma letra maiúscula, uma letra minúscula, um número, um símbolo (ex: @, #, !, etc.) e ter no mínimo 8 caracteres.")]
    string Password
);