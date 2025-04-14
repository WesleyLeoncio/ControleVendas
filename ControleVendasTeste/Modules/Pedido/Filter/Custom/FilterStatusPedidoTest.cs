using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendasTeste.Modules.Pedido.Filter.Interfaces;

namespace ControleVendasTeste.Modules.Pedido.Filter.Custom;

public class FilterStatusPedidoTest : IFilterPedidoResultTest
{
    public List<PedidoEntity> RunFilter(List<PedidoEntity> pedidos, PedidoFiltroRequest filtro)
    {
        if (filtro.VerificarStatusPedido())
        {
            pedidos = pedidos
                .Where(p => p.Status.Equals(filtro.Status)).ToList();
            return pedidos;
        }
        return pedidos;
    }
}