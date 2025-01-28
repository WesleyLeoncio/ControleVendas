using AutoMapper;
using controle_vendas.modules.categoria.model.entity;
using controle_vendas.modules.categoria.model.request;
using controle_vendas.modules.categoria.model.response;

namespace controle_vendas.modules.categoria.model.mapper;

public class CategoriaMapper : Profile
{
    public CategoriaMapper()
    {
        CreateMap<CategoriaRequest, Categoria>();
        CreateMap<Categoria, CategoriaResponse>();
        CreateMap<Categoria, CategoriaProdutoResponse>();
    }
}