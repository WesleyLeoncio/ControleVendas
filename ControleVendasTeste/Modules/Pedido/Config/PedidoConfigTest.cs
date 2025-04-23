using System.Security.Claims;
using AutoMapper;
using ControleVendas.Modules.Cliente.Models.Mapper;
using ControleVendas.Modules.Pedido.Models.Mapper;
using ControleVendasTeste.Config;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ControleVendasTeste.Modules.Pedido.Config;

public class PedidoConfigTest
{
    public readonly Mock<IHttpContextAccessor> MockHttpContextAccessor;
    public readonly IMapper Mapper;

    public PedidoConfigTest()
    {
        MockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        string userId = "vendedor123";
       
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        };
        var identity = new ClaimsIdentity(claims);
        mockHttpContext.User = new ClaimsPrincipal(identity);
        
         Mapper = AutoMapperConfig.Configure(new List<Profile>()
        {
            new PedidoMapper(),
            new ClienteMapper()
        });
       
        MockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);
        
    }
}