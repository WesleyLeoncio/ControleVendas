using System.Linq.Expressions;
using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Mapper;
using ControleVendas.Modules.Produto.Repository.Interfaces;
using ControleVendas.Modules.Produto.Service;
using ControleVendas.Modules.Produto.Service.Interfaces;
using ControleVendasTeste.Config;
using ControleVendasTeste.Modules.Produto.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Produto.Test;

public class DeleteProdutoTest
{
    private readonly IProdutoService _produtoService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public DeleteProdutoTest()
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<IProdutoRepository> mockProdutoRepository = new Mock<IProdutoRepository>();
        var mapper = AutoMapperConfig.Configure(new List<Profile>()
        {
            new ProdutoMapper()
        });

        _produtoService = new ProdutoService(_mockUof.Object, mapper);
        _mockUof.Setup(u => u.ProdutoRepository).Returns(mockProdutoRepository.Object); 
    }
    
    [Fact(DisplayName = "Deve deletar um produto com sucesso")]
    public async Task DeleteProduto_Success()
    {
        // Arrange
        var id = 1;
        var produto = ProdutosData.GetProdutoIndex(0);
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<ProdutoEntity, bool>>>()))
            .ReturnsAsync(produto);
    
        // Act
        await _produtoService.DeleteProduto(id);
    
        // Assert
        _mockUof.Verify(u => u.ProdutoRepository.Delete(It.IsAny<ProdutoEntity>()), Times.Once);
       
        _mockUof.Verify(u => u.Commit(), Times.Once);
    }
    
    [Fact(DisplayName = "Deve lançar exceção NotFoundException ao tentar " +
                        "deletar um produto que não existe")]
    public async Task DeleteCliente_Throws_NotFoundException()
    {
        // Arrange
        var id = 1;
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<ProdutoEntity, bool>>>()))
            .ReturnsAsync(null as ProdutoEntity);
    
        // Act
        Func<Task> act = async () => await _produtoService.DeleteProduto(id);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Produto não encontrado!");
    }
}