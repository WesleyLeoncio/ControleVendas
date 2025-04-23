using AutoMapper;
using ControleVendas.Modules.Produto.Models.Mapper;
using ControleVendasTeste.Config;


namespace ControleVendasTeste.Modules.Produto.Config;

public class ProdutoConfigTest
{
    public IMapper Mapper;
   
    public ProdutoConfigTest()
    {
        Mapper = AutoMapperConfig.Configure(new List<Profile>()
        {
            new ProdutoMapper()
        });
        
    }
}