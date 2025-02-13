using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.fornecedor.model.entity;
using Microsoft.EntityFrameworkCore;

namespace controle_vendas.modules.produto.models.entity;

[Table("produtos")]
[Index(nameof(Nome), IsUnique = true)]
public class Produto
{
    [Key]
    [Column(name:"id")]
    public int Id { get; set; }
    
    [Column(name:"nome")] 
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
    
    [Column(name:"categoria_id")] 
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
    
    [Column(name:"fornecedor_id")] 
    public int FornecedorId { get; set; }
    public Fornecedor? Fornecedor { get; set; }
    
    [Column(name:"valor_compra")]
    [Required]
    public decimal ValorCompra { get; set; } 
    
    [Column(name:"valor_venda")] 
    [Required]
    public decimal ValorVenda { get; set; }
    
    [Column(name:"descricao")] 
    [StringLength(280)]
    public string? Descricao { get; set; }
    
    [Column(name:"estoque")] 
    [Required]
    public int Estoque { get; set; }
    
    [Column(name:"data_cadastro")] 
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DataCadastro { get; set; }
}