using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleVendas.Modules.Produto.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ControleVendas.Modules.Categoria.Models.Entity;

[Table("categorias")]
[Index(nameof(Nome), IsUnique = true)]
public class CategoriaEntity
{
    [Key]
    [Column(name:"id")]
    public int Id { get; set; }
    
    [Column(name:"nome")] 
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
    
    public ICollection<ProdutoEntity> Produtos { get; set; } = new Collection<ProdutoEntity>();
}