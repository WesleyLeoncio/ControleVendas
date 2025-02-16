using ControleVendas.Modules.Common.Pagination;
using ControleVendas.Modules.Pedido.Models.Entity;

namespace ControleVendas.Modules.Pedido.Models.Response;

public class PedidoPaginationResponse
{
    public IEnumerable<PedidoResponse> Pedidos { get; set; }
    
    public MetaData<PedidoEntity> MetaData { get; private set; }

    public PedidoPaginationResponse(IEnumerable<PedidoResponse> pedidos, MetaData<PedidoEntity> metaData)
    {
        Pedidos = pedidos;
        MetaData = metaData;
    }
}