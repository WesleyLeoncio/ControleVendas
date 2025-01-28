using AutoMapper;
using controle_vendas.modules.produto.models.entity;
using controle_vendas.modules.produto.models.request;
using controle_vendas.modules.produto.models.response;

namespace controle_vendas.modules.produto.models.mapper;

public class ProdutoMapper : Profile
{

    public ProdutoMapper()
    {
        CreateMap<ProdutoRequest, Produto>();
        CreateMap<Produto, ProdutoResponse>();
        CreateMap<Produto, ProdutoCategoriaResponse>();
    }
    
}