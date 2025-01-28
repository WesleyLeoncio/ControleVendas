using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using controle_vendas.modules.produto.models.entity;
using Microsoft.EntityFrameworkCore;

namespace controle_vendas.modules.categoria.model.entity;

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
    
    public ICollection<Produto> Produtos { get; set; } = new Collection<Produto>();
}