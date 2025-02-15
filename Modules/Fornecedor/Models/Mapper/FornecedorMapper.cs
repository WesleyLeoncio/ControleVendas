using AutoMapper;
using ControleVendas.Modules.Fornecedor.Models.Request;
using ControleVendas.Modules.Fornecedor.Models.Response;

namespace ControleVendas.Modules.Fornecedor.Models.Mapper;

public class FornecedorMapper : Profile
{
    public FornecedorMapper()
    {
        CreateMap<FornecedorRequest, Entity.Fornecedor>();
        CreateMap<Entity.Fornecedor, FornecedorResponse>();
    }
}