using AutoMapper;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Models.Response;

namespace ControleVendas.Modules.Pedido.Models.Mapper;

public class PedidoMapper : Profile
{
    public PedidoMapper()
    {
        CreateMap<PedidoRequest, Entity.Pedido>()
            .ForMember(dest => dest.Itens, opt => opt.Ignore());
        CreateMap<Entity.Pedido, PedidoResponse>();
    }
}