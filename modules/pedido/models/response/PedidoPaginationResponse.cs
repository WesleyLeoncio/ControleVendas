

using controle_vendas.modules.common.pagination;
using controle_vendas.modules.pedido.models.entity;

namespace controle_vendas.modules.pedido.models.response;

public class PedidoPaginationResponse
{
    public IEnumerable<PedidoResponse> Pedidos { get; set; }
    
    public MetaData<Pedido> MetaData { get; private set; }

    public PedidoPaginationResponse(IEnumerable<PedidoResponse> pedidos, MetaData<Pedido> metaData)
    {
        Pedidos = pedidos;
        MetaData = metaData;
    }
}