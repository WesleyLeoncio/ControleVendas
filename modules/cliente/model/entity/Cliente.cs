using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace controle_vendas.modules.cliente.model.entity;

[Table("cliente")]
[Index(nameof(Telefone), IsUnique = true)]
public class Cliente
{
    [Key]
    [Column(name:"id")]
    public int Id { get; set; }
    
    [Column(name:"nome")] 
    [Required]
    [StringLength(180)]
    public string? Nome { get; set; }
    
    [Column(name:"email")] 
    public string? Email { get; set; }
    
    [Required]
    [Column(name:"telefone")] 
    public string? Telefone { get; set; }
    
    [Column(name:"ativo")]
    [Required]
    public bool Ativo { get; set; }
}