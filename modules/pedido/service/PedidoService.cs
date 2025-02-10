using AutoMapper;
using controle_vendas.modules.common.unit_of_work.interfaces;
using controle_vendas.modules.pedido.models.entity;
using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.pedido.service.interfaces;


namespace controle_vendas.modules.pedido.service;

public class PedidoService : IPedidoService
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public PedidoService(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    public async Task<Pedido> CreatePedido(PedidoRequest request)
    {
        Pedido pedido = _mapper.Map<Pedido>(request);
        return pedido;
    }
}