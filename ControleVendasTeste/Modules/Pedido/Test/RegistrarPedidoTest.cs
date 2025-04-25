using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Pedido.Models.Entity;
using ControleVendas.Modules.Pedido.Models.Request;
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

public class RegistrarPedidoTest : IClassFixture<PedidoConfigTest>
{
    private readonly IPedidoService _pedidoService;
    private readonly Mock<IUnitOfWork> _mockUof;
    
    public RegistrarPedidoTest(PedidoConfigTest pedidoConfigTest)
    {
        _mockUof = new Mock<IUnitOfWork>();
        
        Mock<IPedidoRepository> mockPedidoRepository = new Mock<IPedidoRepository>();
        Mock<IProdutoRepository> mockProdutoRepository = new Mock<IProdutoRepository>();
       
        _pedidoService = new PedidoService(_mockUof.Object, pedidoConfigTest.Mapper,
            pedidoConfigTest.MockHttpContextAccessor.Object);
        _mockUof.Setup(u => u.ProdutoRepository).Returns(mockProdutoRepository.Object); 
        _mockUof.Setup(u => u.PedidoRepository).Returns(mockPedidoRepository.Object);
        
    }

    [Fact(DisplayName = "Deve Registrar um pedido com sucesso.")]
    public async Task RegistrarPedido_Sucesso()
    {   
        // Arrange
        PedidoRequest request = PedidosData.GetPedidoRequest();
        ProdutoEntity produto = ProdutosData.GetProdutoIndex(1);
        produto.Estoque = 10;
        int estoqueInicial = produto.Estoque;
        int quantidadePedido = request.Itens.Sum(i => i.Quantidade); 
        
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<ProdutoEntity, bool>>>()))
            .ReturnsAsync(produto);
        
        _mockUof.Setup(u => u.ProdutoRepository.Update(It.IsAny<ProdutoEntity>()))
            .Callback<ProdutoEntity>(updatedProduto => 
            {
                produto.Estoque = updatedProduto.Estoque; // Atualiza o mock
            });
        
        _mockUof.Setup(u => u.PedidoRepository.Create(It.IsAny<PedidoEntity>()))
            .Returns((PedidoEntity e) => e);

        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);
        
        // Act
        await _pedidoService.RegistrarPedido(request);

        // Assert
        _mockUof.Verify(u => u.PedidoRepository.Create(It.IsAny<PedidoEntity>()), Times.Once);
        _mockUof.Verify(u => u.ProdutoRepository.Update(It.IsAny<ProdutoEntity>()), Times.Once);
        _mockUof.Verify(u => u.Commit(), Times.AtLeastOnce());
       
        _mockUof.Verify(u => u.PedidoRepository.Create(It.Is<PedidoEntity>(p => 
            p.ClienteId == request.ClienteId && 
            p.VendedorId == "vendedor123" && 
            p.Itens.Count == request.Itens.Count
        )), Times.Once);
        
        Assert.Equal(estoqueInicial - quantidadePedido, produto.Estoque);
    }
    
    [Fact(DisplayName = "Deve falhar quando estoque for insuficiente")]
    public async Task RegistrarPedido_EstoqueInsuficiente_Throws_NotFoundException()
    {
        // Arrange
        PedidoRequest request = PedidosData.GetPedidoRequest();
        ProdutoEntity produto = ProdutosData.GetProdutoIndex(1);
        produto.Estoque = 0; // Estoque insuficiente

        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<ProdutoEntity, bool>>>()))
            .ReturnsAsync(produto);
    
        // Act
        Func<Task> act = async () => await _pedidoService.RegistrarPedido(request);
       
       // Assert
       await act.Should().ThrowAsync<NotFoundException>()
           .WithMessage($"Não a produtos o suficiente em estoque, "
                        + $"Quantidade em estoque: {produto.Estoque}");
    }
    
    [Fact(DisplayName = "Deve falhar quando não encontrar o produto")]
    public async Task RegistrarPedido_ProdutoInexistente_Throws_NotFoundException()
    {
        // Arrange
         PedidoRequest request = PedidosData.GetPedidoRequest();

        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<ProdutoEntity, bool>>>()))
            .ReturnsAsync(null as ProdutoEntity);
    
        // Act
        Func<Task> act = async () => await _pedidoService.RegistrarPedido(request);
       
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Produto não encontrado!");
    }

    [Fact(DisplayName = "Deve falhar ao tentar pagar com um valor negativo")]
    public async Task RegistrarPedido_PagamentoNegativo_Throws_ArgumentException()
    {
        // Arrange
        PedidoRequest request = PedidosData.GetPedidoRequest();
        request.Pagamento = -1;
        ProdutoEntity produto = ProdutosData.GetProdutoIndex(1);
        produto.Estoque = 2; 

        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<ProdutoEntity, bool>>>()))
            .ReturnsAsync(produto);
    
        // Act
        Func<Task> act = async () => await _pedidoService.RegistrarPedido(request);
       
        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("O valor do pagamento não pode ser negativo.");
    }

}