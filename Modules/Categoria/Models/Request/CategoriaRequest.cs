using System.ComponentModel.DataAnnotations;

namespace ControleVendas.Modules.Categoria.Models.Request;

public record CategoriaRequest(
    [Required(ErrorMessage = "Campo Nome Obrigatorio!")]
    [StringLength(80, ErrorMessage = "O campo Nome deve ter no máximo 80 caracteres!")]
    string Nome
);