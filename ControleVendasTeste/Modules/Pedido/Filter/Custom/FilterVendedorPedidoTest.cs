using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendasTeste.Modules.Pedido.Filter.Interfaces;

namespace ControleVendasTeste.Modules.Pedido.Filter.Custom;

public class FilterVendedorPedidoTest : IFilterPedidoResultTest
{
    public List<PedidoEntity> RunFilter(List<PedidoEntity> pedidos, PedidoFiltroRequest filtro)
    {
        if (!string.IsNullOrEmpty(filtro.VendedorId))
        {
            pedidos = pedidos
                .Where(p => p.VendedorId == filtro.VendedorId).ToList();
            return pedidos;
        }

        return pedidos;
    }
}