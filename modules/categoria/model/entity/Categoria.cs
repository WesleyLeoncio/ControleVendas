using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace controle_vendas.modules.categoria.model.entity;

[Table("categorias")]
public class Categoria
{
    [Key]
    [Column(name:"id")]
    public int Id { get; set; }
    
    [Column(name:"nome")] 
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
    
    //public ICollection<Produto> Produtos { get; set; } = new Collection<Produto>();
}