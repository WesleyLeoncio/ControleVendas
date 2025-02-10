using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using controle_vendas.modules.cliente.model.entity;
using controle_vendas.modules.item_pedido.models.entity;
using controle_vendas.modules.pedido.models.enums;
using controle_vendas.modules.user.models.entity;

namespace controle_vendas.modules.pedido.models.entity;

[Table("pedidos")]
public class Pedido
{
    [Key]
    [Column(name:"id")]
    public int Id { get; set; }
    
    [Column(name:"cliente_id")] 
    public int ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    [Column(name: "vendedor_id")]
    [StringLength(64)]
    public string? VendedorId { get; set; } = string.Empty;
    public ApplicationUser? Vendedor { get; set; }
    
    public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

    [Column(name: "forma_pagamento")] 
    [Required]
    [StringLength(50)]
    public string? FormaPagamento { get; set; }
    
    [Column(name:"numero_parcelas")] 
    public int NumeroParcela { get; set; }

    [Column(name: "desconto")] public decimal Desconto { get; set; }

    [Column(name: "status_pedido")] public StatusPedido StatusPedido { get; set; } = StatusPedido.Pendente;
    
    [Column(name:"data_venda")] 
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DataVenda { get; set; } = DateTime.Now;
    
    
    
}