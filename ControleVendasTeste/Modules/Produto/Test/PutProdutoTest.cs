using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Repository.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Repository.Interfaces;
using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Repository.Interfaces;
using ControleVendas.Modules.Produto.Service;
using ControleVendas.Modules.Produto.Service.Interfaces;
using ControleVendasTeste.Modules.Categoria.Models;
using ControleVendasTeste.Modules.Fornecedor.Models;
using ControleVendasTeste.Modules.Produto.Config;
using ControleVendasTeste.Modules.Produto.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Produto.Test;

public class PutProdutoTest : IClassFixture<ProdutoConfigTest>
{
    private readonly IProdutoService _produtoService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public PutProdutoTest(ProdutoConfigTest produtoConfigTest)
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<IProdutoRepository> mockProdutoRepository = new Mock<IProdutoRepository>();
        Mock<ICategoriaRepository> mockCategoriaRepository = new Mock<ICategoriaRepository>();
        Mock<IFornecedorRepository> mockFornecedorRepository = new Mock<IFornecedorRepository>();
        

        _produtoService = new ProdutoService(_mockUof.Object, produtoConfigTest.Mapper);
        _mockUof.Setup(u => u.ProdutoRepository).Returns(mockProdutoRepository.Object); 
        _mockUof.Setup(u => u.CategoriaRepository).Returns(mockCategoriaRepository.Object);
        _mockUof.Setup(u => u.FornecedorRepository).Returns(mockFornecedorRepository.Object);
    }

    [Fact(DisplayName = "Deve atualizar um produto com sucesso")]
    public async Task UpdateProduto_Success()
    {
        // Arrange
        int id = 1;
        var request = ProdutosData.GetProdutoRequest();
        var produtoExistente = ProdutosData.GetProdutoIndex(0);
        var categoria = CategoriasData.GetCategoriaIndex(0);
        var fornecedor = FornecedoresData.GetFornecedorIndex(0);
        
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<ProdutoEntity, bool>>>()))
            .ReturnsAsync(produtoExistente);
        
        _mockUof.Setup(u => u.CategoriaRepository.GetAsync(It.IsAny<Expression<Func<CategoriaEntity, bool>>>()))
            .ReturnsAsync(categoria);
        
        _mockUof.Setup(u => u.FornecedorRepository.GetAsync(It.IsAny<Expression<Func<FornecedorEntity, bool>>>()))
            .ReturnsAsync(fornecedor);
        
        // Act
        await _produtoService.UpdateProduto(id, request);
        
        // Assert
        _mockUof.Verify(u => u.ProdutoRepository.Update(It.IsAny<ProdutoEntity>()), Times.Once);
       
        _mockUof.Verify(u => u.Commit(), Times.Once);
    }
    
    [Fact(DisplayName = "Deve lançar exceção ao tentar atualizar com um nome já existente")]
    public async Task UpdateProduto_Throws_KeyDuplicationException()
    {
        // Arrange
        int id = 1;
        var request = new ProdutoRequest("Produto 2", 20, 25,
            "Descrição do produto 1", 20, 1, 2);
        var produtoExistente = ProdutosData.GetProdutoIndex(0);
        var produtoDuplicada = ProdutosData.GetProdutoIndex(1);
        
        _mockUof.SetupSequence(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<ProdutoEntity, bool>>>()))
            .ReturnsAsync(produtoExistente) // Primeiro GetAsync (CheckProduto)
            .ReturnsAsync(produtoDuplicada); // Segundo GetAsync (CheckNameExists)

        // Act
        Func<Task> act = async () => await _produtoService.UpdateProduto(id,request);

        // Assert
        await act.Should().ThrowAsync<KeyDuplicationException>();
    }
    
    [Theory(DisplayName = "Deve testar se o método update de produto " +
                          "lança uma exception NotFoundException ao tentar alterar " +
                          "produtos com categoria ou fornecedor que não existe")]
    [InlineData(2,2,1,"Produto não encontrado!" )]
    [InlineData(1,2,1,"Categoria não encontrada!" )]
    [InlineData(1,1,1,"Fornecedor não encontrado!" )]
    public async Task PutProduto_Throws_NotFoundException(int produtoId,int categoriaId, int fornecedorId, string message)
    {
        //Arrange
        int id = 1;
        var produto = ProdutosData.GetProdutoIndex(0);
        var produtoRequest = ProdutosData.GetProdutoRequest();
        var categoria = CategoriasData.GetCategoriaIndex(0);
        var fornecedor = FornecedoresData.GetFornecedorIndex(0);
        
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(
                It.Is<Expression<Func<ProdutoEntity, bool>>>(expr => 
                    expr.Compile()(new ProdutoEntity{ Id = produtoId}))))
            .ReturnsAsync(produto);
        
        _mockUof.Setup(u => u.CategoriaRepository.GetAsync(
                It.Is<Expression<Func<CategoriaEntity, bool>>>(expr => 
                    expr.Compile()(new CategoriaEntity{ Id = categoriaId}))))
            .ReturnsAsync(categoria);
        
        _mockUof.Setup(u => u.FornecedorRepository.GetAsync(
                It.Is<Expression<Func<FornecedorEntity, bool>>>(expr => 
                    expr.Compile()(new FornecedorEntity(){ Id = fornecedorId }))))
            .ReturnsAsync(fornecedor);
        
        
        //Act
        Func<Task> act = async () => await _produtoService.UpdateProduto(id, produtoRequest);
        
        //Assert
        act.Should().NotBeNull();
        await act.Should().ThrowAsync<NotFoundException>().WithMessage(message);

    }
}