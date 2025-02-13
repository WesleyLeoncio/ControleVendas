using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
    [JsonIgnore]
    public Cliente? Cliente { get; set; }

    [Column(name: "vendedor_id")]
    [StringLength(64)]
    public string? VendedorId { get; set; } = string.Empty;
    [JsonIgnore]
    public ApplicationUser? Vendedor { get; set; }
    
    [JsonIgnore]
    public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

    [Column(name: "forma_pagamento")] 
    [Required]
    public MetodoPagamento FormaPagamento { get; set; }
    
    [Column(name:"numero_parcelas")] 
    public int NumeroParcelas { get; set; }

    [Column(name: "desconto")] public decimal Desconto { get; set; }

    [Column(name: "status")] 
    public StatusPedido Status { get; set; } = StatusPedido.Pendente;
    
    [Column(name:"data_venda")] 
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DataVenda { get; set; }
    
    [Column(name:"valor_pago")]
    public decimal ValorPago { get; set; }

    [Column(name: "valor_total")]
    [Required]
    public decimal ValorTotal
    {
        get => _valorTotal;
        private set => _valorTotal = value;
    }
    
    private decimal _valorTotal;
    
    public void CalcularValorTotal()
    {
        ValorTotal = Itens.Sum(i => i.PrecoTotal) - Desconto;
    }
}