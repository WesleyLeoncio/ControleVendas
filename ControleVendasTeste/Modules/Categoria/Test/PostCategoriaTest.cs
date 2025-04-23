using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Categoria.Models.Response;
using ControleVendas.Modules.Categoria.Service.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendasTeste.Modules.Categoria.Config;
using ControleVendasTeste.Modules.Categoria.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Categoria.Test;

public class PostCategoriaTest : IClassFixture<CategoriaConfigTest>
{
    private readonly ICategoriaService _categoriaService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public PostCategoriaTest(CategoriaConfigTest categoriaConfigTest)
    {
        _categoriaService = categoriaConfigTest.CategoriaService;
        _mockUof = categoriaConfigTest.MockUof;
    }

    [Theory(DisplayName = "Deve testar se o metodo cadastro de categoria retorna CategoriaResponse")]
    [MemberData(nameof(CategoriasData.CategoriasRequest), MemberType = typeof(CategoriasData))]
    public async Task PostCategoria_Returns_CategoriaResponse(CategoriaRequest categoriaRequest)
    {
        //Arrange
        _mockUof.Setup(u => u.CategoriaRepository.Create(It.IsAny<CategoriaEntity>()))
            .Returns((CategoriaEntity c) => c);
        // Configura o Commit para não fazer nada (simulação)
        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);
       
        //Act
        CategoriaResponse act = await _categoriaService.CreateCategoria(categoriaRequest);
        
        //Assert
        act.Should().NotBeNull();
        act.Should().BeOfType<CategoriaResponse>();
        act.Nome.Should().Be(categoriaRequest.Nome);  
        
    }
    
    [Theory(DisplayName = "Deve testar se o metodo cadastro de categoria " +
                          "lança uma exeption KeyDuplicationException ao tentar cadastrar " +
                          "categoria com o mesmo nome")]
    [InlineData("Perfume")]
    public async Task PostCategoria_Throws_KeyDuplicationException(string nome)
    {
        //Arrange
        _mockUof.Setup(u => u.CategoriaRepository.Create(It.IsAny<CategoriaEntity>()))
            .Returns((CategoriaEntity c) => c);
        _mockUof.Setup(u => u.CategoriaRepository.GetAsync(It.IsAny<Expression<Func<CategoriaEntity, bool>>>()))
            .ReturnsAsync(CategoriasData.GetCategoriaIndex(0));
        // Configura o Commit para não fazer nada (simulação)
        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);
        
        //Act
        Func<Task> act = async () => await _categoriaService.CreateCategoria(new CategoriaRequest(nome));
        
        //Assert
        await act.Should().ThrowAsync<KeyDuplicationException>();
        
    }
    
}