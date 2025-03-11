using AutoMapper;
using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Categoria.Models.Response;

namespace ControleVendas.Modules.Categoria.Models.Mapper;

public class CategoriaMapper : Profile
{
    public CategoriaMapper()
    {
        CreateMap<CategoriaRequest, CategoriaEntity>();
        CreateMap<CategoriaEntity, CategoriaResponse>();
        CreateMap<CategoriaEntity, CategoriaProdutoResponse>();
    }
}