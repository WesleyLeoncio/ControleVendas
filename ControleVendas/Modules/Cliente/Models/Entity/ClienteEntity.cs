using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ControleVendas.Modules.Cliente.Models.Entity;

[Table("clientes")]
[Index(nameof(Telefone), IsUnique = true)]
public class ClienteEntity
{
    [Key]
    [Column(name:"id")]
    public int Id { get; set; }
    
    [Column(name:"nome")] 
    [Required]
    [StringLength(180)]
    public string? Nome { get; set; }
    
    [Column(name:"email")] 
    [StringLength(150)]
    public string? Email { get; set; }
    
    [Required]
    [Column(name:"telefone")] 
    [StringLength(20)]
    public string? Telefone { get; set; }
    
    [Column(name:"ativo")]
    [Required]
    public bool Ativo { get; set; }
}