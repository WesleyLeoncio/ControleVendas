using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Request;

namespace ControleVendasTeste.Modules.Pedido.Filter.Interfaces;

public interface IFilterPedidoResultTest
{
    public List<PedidoEntity> RunFilter(List<PedidoEntity> pedidos, PedidoFiltroRequest filtro);
}