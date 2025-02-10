using AutoMapper;
using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.pedido.models.response;

namespace controle_vendas.modules.pedido.models.mapper;

public class PedidoMapper : Profile
{
    public PedidoMapper()
    {
        CreateMap<PedidoRequest, Pedido>();
        CreateMap<Pedido, PedidoResponse>();
    }
}