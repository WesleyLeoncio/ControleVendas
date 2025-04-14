using System.Security.Claims;
using AutoMapper;
using ControleVendas.Modules.Cliente.Models.Mapper;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Mapper;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Models.Response;
using ControleVendas.Modules.Pedido.Repository.Interfaces;
using ControleVendas.Modules.Pedido.Service;
using ControleVendas.Modules.Pedido.Service.Interfaces;
using ControleVendasTeste.Config;
using ControleVendasTeste.Modules.Pedido.Filter.Custom;
using ControleVendasTeste.Modules.Pedido.Filter.Interfaces;
using ControleVendasTeste.Modules.Pedido.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using X.PagedList.Extensions;

namespace ControleVendasTeste.Modules.Pedido.Test;

public class GetAllFilterPedidosTest
{
    private readonly IPedidoService _pedidoService;
    private readonly Mock<IUnitOfWork> _mockUof;
    
    public GetAllFilterPedidosTest()
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<IHttpContextAccessor> mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        DefaultHttpContext mockHttpContext = new DefaultHttpContext();
        string userId = "vendedor123";
       
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId)
        };
        var identity = new ClaimsIdentity(claims);
        mockHttpContext.User = new ClaimsPrincipal(identity);
        Mock<IPedidoRepository> mockPedidoRepository = new Mock<IPedidoRepository>();
        var mapper = AutoMapperConfig.Configure(new List<Profile>()
        {
            new PedidoMapper(),
            new ClienteMapper()
        });
       
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);
       
        _pedidoService = new PedidoService(_mockUof.Object, mapper,mockHttpContextAccessor.Object);
        _mockUof.Setup(u => u.PedidoRepository).Returns(mockPedidoRepository.Object);
    }
    
    [Fact(DisplayName = "Deve retornar um lista de pedido ao buscar pedidos")]
    public async Task GetAllFilterPedidos_Returns_PedidoPaginationResponse()
    {
        // Arrange
        _mockUof.Setup(u => u.PedidoRepository.GetAllIncludeClienteFilterPageableAsync(It.IsAny<PedidoFiltroRequest>()))
            .ReturnsAsync((PedidoFiltroRequest request) =>
            {
                List<PedidoEntity> pedidos = PedidosData.GetListPedidos().ToList();
                return pedidos.ToPagedList(request.PageNumber, request.PageSize);
            });
    
        PedidoFiltroRequest request = new PedidoFiltroRequest();
    
        // Act
        PedidoPaginationResponse act = await _pedidoService.GetAllFilterPedidos(request);
        
        // Assert
        act.Should().NotBeNull();
        act.Pedidos.Should().HaveCount(PedidosData.GetListPedidos().Count);
    }
    
    [Theory(DisplayName = "Deve testar os filtros e retornar uma lista de pedidos")]
    [MemberData(nameof(PedidosData.PeididoFiltroRequestData), MemberType = typeof(PedidosData))]
    public async Task GetAllFilterProdutos_Test_Filter(PedidoFiltroRequest request, int quantidadePedido)
    {
        // Arrange
        List<PedidoEntity> pedidosList = PedidosData.GetListPedidos();

        IEnumerable<IFilterPedidoResultTest> filterResults = new List<IFilterPedidoResultTest>
        {
            new FilterVendedorPedidoTest(),
            new FilterNameClientePedidoTest(),
            new FilterStatusPedidoTest()
        };
       
        foreach (var filter in filterResults)
        {
            pedidosList = filter.RunFilter(pedidosList, request);
        }
        
        _mockUof.Setup(u => u.PedidoRepository.GetAllIncludeClienteFilterPageableAsync(It.IsAny<PedidoFiltroRequest>()))
            .ReturnsAsync(() => pedidosList.ToPagedList(request.PageNumber, request.PageSize));
        
        
        // Act
        PedidoPaginationResponse act = await _pedidoService.GetAllFilterPedidos(request);
        
        // Assert
        act.Should().NotBeNull();
        act.Pedidos.Should().NotBeEmpty();
        
        if (!string.IsNullOrEmpty(request.Nome)) act.Pedidos.Should().OnlyContain(pedidoResponse => 
            pedidoResponse.Cliente.Nome.Contains(request.Nome, StringComparison.OrdinalIgnoreCase));
            
        act.Pedidos.Should().HaveCount(quantidadePedido);
    }
   

}