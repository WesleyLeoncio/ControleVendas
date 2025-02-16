using AutoMapper;
using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Models.Response;

namespace ControleVendas.Modules.Produto.Models.Mapper;

public class ProdutoMapper : Profile
{

    public ProdutoMapper()
    {
        CreateMap<ProdutoRequest, ProdutoEntity>();
        CreateMap<ProdutoEntity, ProdutoResponse>();
        CreateMap<ProdutoEntity, ProdutoCategoriaResponse>();
    }
    
}