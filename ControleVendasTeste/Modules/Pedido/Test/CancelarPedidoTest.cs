using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Enums;
using ControleVendas.Modules.Pedido.Repository.Interfaces;
using ControleVendas.Modules.Pedido.Service;
using ControleVendas.Modules.Pedido.Service.Interfaces;
using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Repository.Interfaces;
using ControleVendasTeste.Modules.Pedido.Config;
using ControleVendasTeste.Modules.Pedido.Models;
using ControleVendasTeste.Modules.Produto.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Pedido.Test;

public class CancelarPedidoTest : IClassFixture<PedidoConfigTest>
{
    private readonly IPedidoService _pedidoService;
    private readonly Mock<IUnitOfWork> _mockUof;
    
    public CancelarPedidoTest(PedidoConfigTest pedidoConfigTest)
    {
        _mockUof = new Mock<IUnitOfWork>();
        
        Mock<IPedidoRepository> mockPedidoRepository = new Mock<IPedidoRepository>();
        Mock<IProdutoRepository> mockProdutoRepository = new Mock<IProdutoRepository>();
       
        _pedidoService = new PedidoService(_mockUof.Object, pedidoConfigTest.Mapper,
            pedidoConfigTest.MockHttpContextAccessor.Object);
        _mockUof.Setup(u => u.ProdutoRepository).Returns(mockProdutoRepository.Object); 
        _mockUof.Setup(u => u.PedidoRepository).Returns(mockPedidoRepository.Object);
    }
    
    [Fact(DisplayName = "Deve realizar o cancelamento de um pedido com sucesso.")]
    public async Task CancelarPedido_Sucesso()
    {
        // Arrange
        int pedidoId = 1;
        PedidoEntity pedido = PedidosData.GetPedidoIndex(0);
        ProdutoEntity produto = ProdutosData.GetProdutoIndex(0);
        produto.Estoque = 10;
        int estoqueInicial = produto.Estoque;
        int quantidadePedido = pedido.Itens.Sum(i => i.Quantidade); 
        
        _mockUof.Setup(u => u.PedidoRepository.GetPedidosIncludeItensPendentePorId(It.IsAny<int>()))
            .ReturnsAsync(pedido);
        
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<ProdutoEntity, bool>>>()))
            .ReturnsAsync(produto);

        _mockUof.Setup(u => u.PedidoRepository.Update(It.IsAny<PedidoEntity>()));
        
        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);
        
        // Act
        await _pedidoService.CancelarPedido(pedidoId);
        
        // Assert 
        _mockUof.Verify(u => u.PedidoRepository.Update(It.IsAny<PedidoEntity>()), Times.Once);
        _mockUof.Verify(u => u.ProdutoRepository.Update(It.IsAny<ProdutoEntity>()), Times.Once);
        _mockUof.Verify(u => u.Commit(), Times.Once);
        
        _mockUof.Verify(u => u.PedidoRepository.Update(It.Is<PedidoEntity>(p => 
            p.ClienteId == pedido.ClienteId && 
            p.VendedorId == pedido.VendedorId && 
            p.Itens.Count == pedido.Itens.Count &&
            p.Status == pedido.Status
        )), Times.Once);
        
        Assert.Equal(estoqueInicial + quantidadePedido, produto.Estoque);
    }
    
    [Fact(DisplayName = "Deve falhar quando não encontrar o produto")]
    public async Task CancelarPedido_ProdutoInexistente_Throws_NotFoundException()
    {
        // Arrange
        int idPedido = 1;
        PedidoEntity pedido = PedidosData.GetPedidoIndex(0);
        
        _mockUof.Setup(u => u.PedidoRepository.GetPedidosIncludeItensPendentePorId(It.IsAny<int>()))
            .ReturnsAsync(pedido);

        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<ProdutoEntity, bool>>>()))
            .ReturnsAsync(null as ProdutoEntity);
    
        // Act
        Func<Task> act = async () => await _pedidoService.CancelarPedido(idPedido);
       
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Produto não encontrado!");
    }
    
    [Fact(DisplayName = "Deve falhar quando não encontrar o pedido")]
    public async Task CancelarPedido_PedidoInexistente_Throws_NotFoundException()
    {
        // Arrange
        int idPedido = 1;
        
        _mockUof.Setup(u => u.PedidoRepository.GetPedidosIncludeItensPendentePorId(It.IsAny<int>()))
            .ThrowsAsync(new NotFoundException("Pedido não encontrado!"));
        
        // Act
        Func<Task> act = async () => await _pedidoService.CancelarPedido(idPedido);
       
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Pedido não encontrado!");
    }
    
    [Fact(DisplayName = "Deve falhar quando tentar cancelar um pedido com status de cancelado")]
    public async Task CancelarPedido_PedidoCancelado_Throws_ConflictException()
    {
        // Arrange
        int idPedido = 1;
        PedidoEntity pedido = PedidosData.GetPedidoIndex(0);
        pedido.Status = StatusPedido.Cancelado;
        
        _mockUof.Setup(u => u.PedidoRepository.GetPedidosIncludeItensPendentePorId(It.IsAny<int>()))
            .ReturnsAsync(pedido);
        
        // Act
        Func<Task> act = async () => await _pedidoService.CancelarPedido(idPedido);
       
        // Assert
        await act.Should().ThrowAsync<ConflictException>()
            .WithMessage("Pedido já está cancelado!");
    }
}