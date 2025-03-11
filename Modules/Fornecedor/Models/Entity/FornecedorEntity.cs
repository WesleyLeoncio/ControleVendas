using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ControleVendas.Modules.Fornecedor.Models.Entity;

[Table("fornecedores")]
[Index(nameof(Nome), IsUnique = true)]
public class FornecedorEntity
{
    [Key]
    [Column(name:"id")]
    public int Id { get; set; }
    
    [Column(name:"nome")] 
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
}