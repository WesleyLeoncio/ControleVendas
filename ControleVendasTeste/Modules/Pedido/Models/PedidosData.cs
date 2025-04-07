using ControleVendas.Modules.ItemPedido.models.Request;
using ControleVendas.Modules.Pedido.Models.Enums;
using ControleVendas.Modules.Pedido.Models.Request;

namespace ControleVendasTeste.Modules.Pedido.Models;

public static class PedidosData
{
    public static PedidoRequest GetPedidoRequest()
    {
        return new PedidoRequest
        {
            ClienteId = 1,FormaPagamento = MetodoPagamento.AVISTA,NumeroParcelas = 0,
            Desconto = 0,Pagamento = 40,Itens = new List<ItemPedidoRequest>
            {
                new ItemPedidoRequest(1,1),
            }
        };
    }
}