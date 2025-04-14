using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendasTeste.Modules.Pedido.Filter.Interfaces;

namespace ControleVendasTeste.Modules.Pedido.Filter.Custom;

public class FilterNameClientePedidoTest : IFilterPedidoResultTest
{
    public List<PedidoEntity> RunFilter(List<PedidoEntity> pedidos, PedidoFiltroRequest filtro)
    {
        if (string.IsNullOrEmpty(filtro.Nome))
            return pedidos;

        return pedidos
            .Where(p => p.Cliente?.Nome != null &&
                        p.Cliente.Nome.Contains(filtro.Nome, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}