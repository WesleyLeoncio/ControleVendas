using System.Linq.Expressions;
using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Repository.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Repository.Interfaces;
using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Mapper;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Models.Response;
using ControleVendas.Modules.Produto.Repository.Interfaces;
using ControleVendas.Modules.Produto.Service;
using ControleVendas.Modules.Produto.Service.Interfaces;
using ControleVendasTeste.Config;
using ControleVendasTeste.Modules.Categoria.Models;
using ControleVendasTeste.Modules.Fornecedor.Models;
using ControleVendasTeste.Modules.Produto.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Produto.Test;

public class PostProdutoTest
{
    private readonly IProdutoService _produtoService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public PostProdutoTest()
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<IProdutoRepository> mockProdutoRepository = new Mock<IProdutoRepository>();
        Mock<ICategoriaRepository> mockCategoriaRepository = new Mock<ICategoriaRepository>();
        Mock<IFornecedorRepository> mockFornecedorRepository = new Mock<IFornecedorRepository>();
        var mapper = AutoMapperConfig.Configure(new List<Profile>()
        {
            new ProdutoMapper()
        });

        _produtoService = new ProdutoService(_mockUof.Object, mapper);
        _mockUof.Setup(u => u.ProdutoRepository).Returns(mockProdutoRepository.Object); 
        _mockUof.Setup(u => u.CategoriaRepository).Returns(mockCategoriaRepository.Object);
        _mockUof.Setup(u => u.FornecedorRepository).Returns(mockFornecedorRepository.Object);
    }
    
    [Theory(DisplayName = "Deve testar se o metodo cadastro de produto retorna ProdutoResponse")]
    [MemberData(nameof(ProdutosData.ProdutosRequest), MemberType = typeof(ProdutosData))]
    public async Task PostProduto_Returns_ProdutoResponse(ProdutoRequest produtoRequest)
    {
        //Arrange
        var categoria = CategoriasData.GetCategoriaIndex(0);
        var fornecedor = FornecedoresData.GetFornecedorIndex(0);
        
        _mockUof.Setup(u => u.ProdutoRepository.Create(It.IsAny<ProdutoEntity>()))
            .Returns((ProdutoEntity c) => c);
        
        _mockUof.Setup(u => u.CategoriaRepository.GetAsync(It.IsAny<Expression<Func<CategoriaEntity, bool>>>()))
            .ReturnsAsync(categoria);
        
        _mockUof.Setup(u => u.FornecedorRepository.GetAsync(It.IsAny<Expression<Func<FornecedorEntity, bool>>>()))
            .ReturnsAsync(fornecedor);
        // Configura o Commit para não fazer nada (simulação)
        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);
       
        //Act
        ProdutoResponse act = await _produtoService.CreateProduto(produtoRequest);
        
        //Assert
        act.Should().NotBeNull();
        act.Should().BeOfType<ProdutoResponse>();
        act.Nome.Should().Be(produtoRequest.Nome);  
        
    }
    
    
    //TODO 
    //MODIFICAR OS OUTROS TESTE COM BASE NESSE 
    [Fact(DisplayName = "Deve testar se o metodo cadastro de produto " +
                        "lança uma exeption KeyDuplicationException ao tentar cadastrar " +
                        "produtos com o mesmo nome")]
    public async Task PostProduto_Throws_KeyDuplicationException()
    {
        //Arrange
        var produtoRequest = ProdutosData.GetProdutoRequest();
        var produto = ProdutosData.GetProdutoIndex(0);
        produto.Nome = produtoRequest.Nome;
        
        // Ajustando o mock para verificar a expressão de nome do produto
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(
                It.Is<Expression<Func<ProdutoEntity, bool>>>(expr => 
                    expr.Compile()(new ProdutoEntity { Nome = produto.Nome }))))
            .ReturnsAsync(produto);
   
        //Act
        Func<Task> act = async () => await _produtoService.CreateProduto(produtoRequest);
        
        //Assert
        await act.Should().ThrowAsync<KeyDuplicationException>()
            .WithMessage("Já existe um produto com este nome!");
    }
  
    
}