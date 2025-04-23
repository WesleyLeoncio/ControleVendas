using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Service.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendasTeste.Modules.Categoria.Config;
using ControleVendasTeste.Modules.Categoria.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Categoria.Test;

public class DeleteCategoriaTest : IClassFixture<CategoriaConfigTest>
{
    private readonly ICategoriaService _categoriaService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public DeleteCategoriaTest(CategoriaConfigTest categoriaConfigTest)
    {
        _categoriaService = categoriaConfigTest.CategoriaService;
        _mockUof = categoriaConfigTest.MockUof;
    }
    
    [Fact(DisplayName = "Deve deletar uma categoria com sucesso")]
    public async Task DeleteCategoria_Success()
    {
        // Arrange
        var id = 1;
        
        _mockUof.Setup(u => u.CategoriaRepository.GetAsync(It.IsAny<Expression<Func<CategoriaEntity, bool>>>()))
            .ReturnsAsync(CategoriasData.GetCategoriaIndex(0));
    
        // Act
        await _categoriaService.DeleteCategoria(id);
    
        // Assert
        _mockUof.Verify(u => u.CategoriaRepository.Delete(It.IsAny<CategoriaEntity>()), Times.Once);
       
        _mockUof.Verify(u => u.Commit(), Times.Once);
    }
    
    [Fact(DisplayName = "Deve lançar exceção NotFoundException ao tentar " +
                        "deletar uma categoria que não existe")]
    public async Task DeleteCategoria_Throws_NotFoundException()
    {
        // Arrange
        var id = 1;
        _mockUof.Setup(u => u.CategoriaRepository.GetAsync(It.IsAny<Expression<Func<CategoriaEntity, bool>>>()))
            .ReturnsAsync(null as CategoriaEntity);
    
        // Act
        Func<Task> act = async () => await _categoriaService.DeleteCategoria(id);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Categoria não encontrada!");
    }
}