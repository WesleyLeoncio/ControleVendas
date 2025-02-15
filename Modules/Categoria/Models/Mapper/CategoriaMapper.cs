using AutoMapper;
using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Categoria.Models.Response;

namespace ControleVendas.Modules.Categoria.Models.Mapper;

public class CategoriaMapper : Profile
{
    public CategoriaMapper()
    {
        CreateMap<CategoriaRequest, Entity.Categoria>();
        CreateMap<Entity.Categoria, CategoriaResponse>();
        CreateMap<Entity.Categoria, CategoriaProdutoResponse>();
    }
}