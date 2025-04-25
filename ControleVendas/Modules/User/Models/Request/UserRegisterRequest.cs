using System.ComponentModel.DataAnnotations;

namespace ControleVendas.Modules.User.Models.Request;

public record UserRegisterRequest(
    [Required(ErrorMessage = "O campo UserName é Obrigatório!")]
    string Username,
    [Required(ErrorMessage = "O campo Nome Completo é Obrigatório!")]
    string FullName,
    [EmailAddress]
    [Required(ErrorMessage = "O campo Email é Obrigatório!")]
    string Email,
    [Required(ErrorMessage = "O campo Password é Obrigatório!")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "A senha deve conter pelo menos: uma letra maiúscula, uma letra minúscula, um número, um símbolo (ex: @, #, !, etc.) e ter no mínimo 8 caracteres.")]
    string Password,
    [Required(ErrorMessage = "Campo Telefone Obrigatório!")]
    [RegularExpression(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$", ErrorMessage = "Número de telefone inválido.")]
    string PhoneNumber
);