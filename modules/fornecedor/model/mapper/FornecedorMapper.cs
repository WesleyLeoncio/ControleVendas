using AutoMapper;
using controle_vendas.modules.fornecedor.model.entity;
using controle_vendas.modules.fornecedor.model.request;
using controle_vendas.modules.fornecedor.model.response;

namespace controle_vendas.modules.fornecedor.model.mapper;

public class FornecedorMapper : Profile
{
    public FornecedorMapper()
    {
        CreateMap<FornecedorRequest, Fornecedor>();
        CreateMap<Fornecedor, FornecedorResponse>();
    }
}