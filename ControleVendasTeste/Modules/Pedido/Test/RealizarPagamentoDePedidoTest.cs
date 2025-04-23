using System.Linq.Expressions;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Enums;
using ControleVendas.Modules.Pedido.Models.Request;
using ControleVendas.Modules.Pedido.Repository.Interfaces;
using ControleVendas.Modules.Pedido.Service;
using ControleVendas.Modules.Pedido.Service.Interfaces;
using ControleVendas.Modules.Produto.Repository.Interfaces;
using ControleVendasTeste.Modules.Pedido.Config;
using ControleVendasTeste.Modules.Pedido.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Pedido.Test;

public class RealizarPagamentoDePedidoTest : IClassFixture<PedidoConfigTest>
{
    private readonly IPedidoService _pedidoService;
    private readonly Mock<IUnitOfWork> _mockUof;
    
    public RealizarPagamentoDePedidoTest(PedidoConfigTest pedidoConfigTest)
    {
        _mockUof = new Mock<IUnitOfWork>();
        
        Mock<IPedidoRepository> mockPedidoRepository = new Mock<IPedidoRepository>();
        Mock<IProdutoRepository> mockProdutoRepository = new Mock<IProdutoRepository>();
       
        _pedidoService = new PedidoService(_mockUof.Object, pedidoConfigTest.Mapper,
            pedidoConfigTest.MockHttpContextAccessor.Object);
        _mockUof.Setup(u => u.ProdutoRepository).Returns(mockProdutoRepository.Object); 
        _mockUof.Setup(u => u.PedidoRepository).Returns(mockPedidoRepository.Object);
    }
    
    [Theory(DisplayName = "Deve realizar o pagamento de um pedido com sucesso.")]
    [InlineData(1,50, StatusPedido.Pago)]
    [InlineData(1,20, StatusPedido.Pendente)]
    [InlineData(1,20, StatusPedido.Atrasado)]
    public async Task RealizarPagamentoDePedido_Sucesso(int id, Decimal pagamento, StatusPedido status)
    {   
        // Arrange
        PedidoPagamentoRequest request = PedidosData.GetPedidoPagamentoRequest(id,pagamento);
        PedidoEntity pedido = PedidosData.GetPedidoIndex(1);
       
        if (status == StatusPedido.Atrasado) pedido.DataVenda = pedido.DataVenda.AddDays(-120);
       
        _mockUof.Setup(u => u.PedidoRepository.GetAsync(It.IsAny<Expression<Func<PedidoEntity, bool>>>()))
            .ReturnsAsync(pedido);
        
        // Act
        await _pedidoService.RealizarPagamentoDePedido(request);
        
        // Assert
        _mockUof.Verify(u => u.PedidoRepository.Update(It.IsAny<PedidoEntity>()), Times.Once);
        _mockUof.Verify(u => u.Commit(), Times.Once);
      
        _mockUof.Verify(u => u.PedidoRepository.Update(It.Is<PedidoEntity>(p => 
            p.ClienteId == pedido.ClienteId && 
            p.VendedorId == pedido.VendedorId && 
            p.Itens.Count == pedido.Itens.Count &&
            p.Status == status
        )), Times.Once);
        
    }
    
    [Fact(DisplayName = "Deve falhar ao tentar pagar com um valor negativo")]
    public async Task RealizarPagamentoDePedido_PagamentoNegativo_Throws_ArgumentException()
    {
        // Arrange
        PedidoPagamentoRequest request = PedidosData.GetPedidoPagamentoRequest(1,-1);
        PedidoEntity pedido = PedidosData.GetPedidoIndex(0);
        
        _mockUof.Setup(u => u.PedidoRepository.GetAsync(It.IsAny<Expression<Func<PedidoEntity, bool>>>()))
            .ReturnsAsync(pedido);
    
        // Act
        Func<Task> act = async () => await _pedidoService.RealizarPagamentoDePedido(request);
       
        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("O valor do pagamento não pode ser negativo.");
    }
}