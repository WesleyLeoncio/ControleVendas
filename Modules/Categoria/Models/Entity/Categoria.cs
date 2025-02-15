using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ControleVendas.Modules.Categoria.Models.Entity;

[Table("categorias")]
[Index(nameof(Nome), IsUnique = true)]
public class Categoria
{
    [Key]
    [Column(name:"id")]
    public int Id { get; set; }
    
    [Column(name:"nome")] 
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
    
    public ICollection<Produto.Models.Entity.Produto> Produtos { get; set; } = new Collection<Produto.Models.Entity.Produto>();
}