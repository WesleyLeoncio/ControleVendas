using ControleVendas.Modules.ItemPedido.models.Entity;
using ControleVendas.Modules.ItemPedido.models.Request;
using ControleVendas.Modules.Pedido.Models.Entity;
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
    
    public static PedidoPagamentoRequest GetPedidoPagamentoRequest(int id, Decimal pagamento)
    {
        return new PedidoPagamentoRequest(id,pagamento);
    }
    

    public static List<PedidoEntity> GetListPedidos()
    {
        return new List<PedidoEntity>
        {
            CriarPedido(
                new PedidoDados
                {
                    Id = 1,
                    ClienteId = 1,
                    VendedorId = "vendedor123",
                    Status = StatusPedido.Pendente,
                    DataVenda = DateTime.Now,
                    Itens = new List<ItemPedidoEntity>
                    {
                        CriarItemPedido(1, 1, 20, 15)
                    }
                },new PagamentoInfo
                {
                    FormaPagamento = MetodoPagamento.AVISTA,
                    NumeroParcelas = 0,
                    Desconto = 0,
                    ValorPago = 0
                }
            ),

            CriarPedido(
                new PedidoDados
                {
                    Id = 2,
                    ClienteId = 2,
                    VendedorId = "vendedor456",
                    Status = StatusPedido.Pendente,
                    DataVenda = DateTime.Now,
                    Itens = new List<ItemPedidoEntity>
                    {
                        CriarItemPedido(1, 1, 20, 15),
                        CriarItemPedido(2, 1, 30, 20)
                    }
                },
                new PagamentoInfo
                {
                    FormaPagamento = MetodoPagamento.PARCELADO,
                    NumeroParcelas = 2,
                    Desconto = 0,
                    ValorPago = 0,
                }
                
            )
        };
    }
    
    public static PedidoEntity GetPedidoIndex(int index)
    {
        return GetListPedidos()[index];
    }
    
    private static PedidoEntity CriarPedido(
        PedidoDados dados,
        PagamentoInfo pagamento)
    {
        var pedido = new PedidoEntity
        {
            Id = dados.Id,
            ClienteId = dados.ClienteId,
            VendedorId = dados.VendedorId,
            Status = dados.Status,
            DataVenda = dados.DataVenda,
            FormaPagamento = pagamento.FormaPagamento,
            NumeroParcelas = pagamento.NumeroParcelas,
            Desconto = pagamento.Desconto,
            ValorPago = pagamento.ValorPago,
            Itens = dados.Itens
        };

        pedido.CalcularValorTotal();
        return pedido;
    }

    private static ItemPedidoEntity CriarItemPedido(
        int produtoId,
        int quantidade,
        decimal precoVenda,
        decimal precoCompra)
    {
        var item = new ItemPedidoEntity
        {
            ProdutoId = produtoId,
            Quantidade = quantidade,
            PrecoUnitario = precoVenda
        };

        item.CalcularLucroItemPedido(precoCompra);
        return item;
    }
    
    
}