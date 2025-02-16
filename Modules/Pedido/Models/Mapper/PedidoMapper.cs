using AutoMapper;
using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Models.Response;

namespace ControleVendas.Modules.Pedido.Models.Mapper;

public class PedidoMapper : Profile
{
    public PedidoMapper()
    {
        CreateMap<PedidoRequest, PedidoEntity>()
            .ForMember(dest => dest.Itens, opt => opt.Ignore());
        CreateMap<PedidoEntity, PedidoResponse>();
    }
}