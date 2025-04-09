using ControleVendas.Modules.ItemPedido.models.Entity;
using ControleVendas.Modules.Pedido.Models.Enums;

namespace ControleVendasTeste.Modules.Pedido.Models;

public class PedidoDados
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string VendedorId { get; set; } = "";
    public StatusPedido Status { get; set; }
    public DateTime DataVenda { get; set; }
    
    public List<ItemPedidoEntity> Itens {  get; set; } = new List<ItemPedidoEntity>();
}