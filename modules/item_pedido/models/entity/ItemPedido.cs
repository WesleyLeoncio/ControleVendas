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
    [Required]
    public decimal? PrecoUnitario
    {
        get => _precoUnitario;
        set
        {
            _precoUnitario = value;
            CalcularPrecoTotal();
        }
    } 
    
    [Column(name:"preco_total")]
    [Required]
    public decimal? PrecoTotal { get; set; } 
    
    [Column(name:"lucro_item")]
    [Required]
    public decimal? LucroItem { get; set; }
    
    private decimal? _precoUnitario;
    private int _quantidade;
    
    private void CalcularPrecoTotal()
    {
        PrecoTotal = (PrecoUnitario ?? 0) * Quantidade;
    }
    
    public void CalcularLucroItemPedido(decimal? produtoValorCompra)
    {
        if (produtoValorCompra < 0) throw new ArgumentException("O valor de compra do produto não pode ser negativo.");
        LucroItem = PrecoTotal - (produtoValorCompra * Quantidade);
    }

}