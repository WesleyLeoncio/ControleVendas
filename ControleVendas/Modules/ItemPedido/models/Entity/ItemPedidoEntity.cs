using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Produto.Models.Entity;

namespace ControleVendas.Modules.ItemPedido.models.Entity;

[Table("itens_pedidos")]
public class ItemPedidoEntity
{
    [Key]
    [Column(name: "id")]
    public int Id { get; set; }
    
    [ForeignKey("Pedido")]
    [Column("pedido_id")]
    public int PedidoId { get; set; }
    public PedidoEntity? Pedido { get; set; }

    [Column(name: "produto_id")]
    [Required]
    public int ProdutoId { get; set; }
    public ProdutoEntity? Produto { get; set; } 
    
    [Column(name: "quantidade")]
    [Required]
    public int Quantidade
    {
        get => _quantidade;
        set
        {
            _quantidade = value;
            CalcularPrecoTotal();
        }
    }

    [Column(name: "preco_unitario")]
    
    public decimal PrecoUnitario
    {
        get => _precoUnitario;
        set
        {
            _precoUnitario = value;
            CalcularPrecoTotal();
        }
    }

    [Column(name: "preco_total")]
    [Required]
    public decimal PrecoTotal { get; private set; }

    [Column(name: "lucro_item")]
    [Required]
    public decimal LucroItem { get; private set; }
    
    
    private int _quantidade;
    private decimal _precoUnitario;

    private void CalcularPrecoTotal()
    {
        PrecoTotal = PrecoUnitario * Quantidade;
    }

    public void CalcularLucroItemPedido(decimal produtoValorCompra)
    {
        if (produtoValorCompra < 0)
        {
            throw new ArgumentException("O valor de compra do produto não pode ser negativo.");
        }
        LucroItem = PrecoTotal - (produtoValorCompra * Quantidade);
    }
}