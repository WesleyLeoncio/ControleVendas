using AutoMapper;
using controle_vendas.modules.cliente.model.entity;
using controle_vendas.modules.cliente.model.request;
using controle_vendas.modules.cliente.model.response;

namespace controle_vendas.modules.cliente.model.mapper;

public class ClienteMapper : Profile
{
    public ClienteMapper()
    {
        CreateMap<ClienteRequest,Cliente>();
        CreateMap<Cliente,ClienteResponse>();
    }
}