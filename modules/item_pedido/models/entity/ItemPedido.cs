using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using controle_vendas.modules.produto.models.entity;

namespace controle_vendas.modules.item_pedido.models.entity;

[Table("itens_pedido")]
public class ItemPedido
{
    [Key]
    [Column(name:"id")]
    public int Id { get; set; }
    
    [Column(name:"produto_id")]
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }
    
    [Column(name:"quantidade")]
    [Required]
    public int Quantidade { get; set; }
    
  
}