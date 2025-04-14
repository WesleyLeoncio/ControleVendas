using System.Security.Claims;
using AutoMapper;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Enums;
using ControleVendas.Modules.Pedido.Models.Mapper;
using ControleVendas.Modules.Pedido.Repository.Interfaces;
using ControleVendas.Modules.Pedido.Service;
using ControleVendas.Modules.Pedido.Service.Interfaces;
using ControleVendasTeste.Config;
using ControleVendasTeste.Modules.Pedido.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace ControleVendasTeste.Modules.Pedido.Test;

public class VerificarPedidosAtrasadosTest
{
    private readonly IPedidoService _pedidoService;
    private readonly Mock<IUnitOfWork> _mockUof;
    
    public VerificarPedidosAtrasadosTest()
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
            new PedidoMapper()
        });
       
        mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(mockHttpContext);

        _pedidoService = new PedidoService(_mockUof.Object, mapper, mockHttpContextAccessor.Object);
        _mockUof.Setup(u => u.PedidoRepository).Returns(mockPedidoRepository.Object);
    } 
    
    [Fact(DisplayName = "Deve verificar se a pedidos atrasados.")]
 
    public async Task VerificarPedidosAtrasados_Com_Sucesso()
    {   
        // Arrange
        IEnumerable<PedidoEntity> pedidos = PedidosData.GetListPedidos();
        pedidos.ElementAt(3).Status = StatusPedido.Pendente;
        pedidos.ElementAt(3).DataVenda = DateTime.Now.AddDays(-120);
        
        _mockUof.Setup(u => u.PedidoRepository.GetAllPedidosStatusPendente())
            .ReturnsAsync(pedidos);
        
        // Act
        await _pedidoService.VerificarPedidosAtrasados();
        
        // Assert
        
        _mockUof.Verify(u => u.PedidoRepository.Update(It.Is<PedidoEntity>(p => 
            p.Status == StatusPedido.Atrasado
        )), Times.Once);
        
    }
    
    [Fact(DisplayName = "Deve verificar se quando não tem pedidos atrasados " +
                        "não é realizado a alteração do status do pedido")]
    public async Task VerificarPedidosAtrasados_Não_Deve_AlterarStatusPedido()
    {
        // Arrange
        IEnumerable<PedidoEntity> pedidos = PedidosData.GetListPedidos();
        
        _mockUof.Setup(u => u.PedidoRepository.GetAllPedidosStatusPendente())
            .ReturnsAsync(pedidos);
    
        // Act
        await _pedidoService.VerificarPedidosAtrasados();
    
        // Assert
        _mockUof.Verify(u => u.PedidoRepository.Update(It.IsAny<PedidoEntity>()), Times.Never);
        _mockUof.Verify(u => u.Commit(), Times.Never);
    }

}