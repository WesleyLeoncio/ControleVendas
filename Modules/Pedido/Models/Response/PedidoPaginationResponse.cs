using ControleVendas.Modules.Common.Pagination;

namespace ControleVendas.Modules.Pedido.Models.Response;

public class PedidoPaginationResponse
{
    public IEnumerable<PedidoResponse> Pedidos { get; set; }
    
    public MetaData<Entity.Pedido> MetaData { get; private set; }

    public PedidoPaginationResponse(IEnumerable<PedidoResponse> pedidos, MetaData<Entity.Pedido> metaData)
    {
        Pedidos = pedidos;
        MetaData = metaData;
    }
}