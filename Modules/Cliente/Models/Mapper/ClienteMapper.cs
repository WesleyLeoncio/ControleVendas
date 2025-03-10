﻿using AutoMapper;
using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Cliente.Models.Response;

namespace ControleVendas.Modules.Cliente.Models.Mapper;

public class ClienteMapper : Profile
{
    public ClienteMapper()
    {
        CreateMap<ClienteRequest,ClienteEntity>();
        CreateMap<ClienteEntity,ClienteResponse>();
    }
}