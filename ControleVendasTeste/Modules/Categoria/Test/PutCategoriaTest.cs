using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Categoria.Service.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendasTeste.Modules.Categoria.Config;
using ControleVendasTeste.Modules.Categoria.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Categoria.Test;

public class PutCategoriaTest : IClassFixture<CategoriaConfigTest>
{
    private readonly ICategoriaService _categoriaService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public PutCategoriaTest(CategoriaConfigTest categoriaConfigTest)
    {
        _categoriaService = categoriaConfigTest.CategoriaService;
        _mockUof = categoriaConfigTest.MockUof;
    }
    
    [Fact(DisplayName = "Deve atualizar uma categoria com sucesso")]
    public async Task UpdateCategoria_Success()
    {
        // Arrange
        var id = 1;
        var request = new CategoriaRequest("Perfume Editado");
        var categoriaExistente = CategoriasData.GetCategoriaIndex(0);
        
        _mockUof.Setup(u => u.CategoriaRepository.GetAsync(It.IsAny<Expression<Func<CategoriaEntity, bool>>>()))
            .ReturnsAsync((Expression<Func<CategoriaEntity, bool>> predicate) =>
            {
                var compiledPredicate = predicate.Compile(); 
                return compiledPredicate(categoriaExistente) ? categoriaExistente : null;
            });
    
        // Act
        await _categoriaService.UpdateCategoria(id, request);
    
        // Assert
        _mockUof.Verify(u => u.CategoriaRepository.Update(It.IsAny<CategoriaEntity>()), Times.Once);
       
        _mockUof.Verify(u => u.Commit(), Times.Once);
    }
    
    [Fact(DisplayName = "Deve lançar exceção ao tentar atualizar com um nome já existente")]
    public async Task UpdateCategoria_Throws_KeyDuplicationException()
    {
       // Arrange
        int id = 1;
        var request = new CategoriaRequest("Desodorante");
        var categoriaExistente = CategoriasData.GetCategoriaIndex(0);
        var categoriaDuplicada = CategoriasData.GetCategoriaIndex(1);
        
        _mockUof.SetupSequence(u => u.CategoriaRepository.GetAsync(It.IsAny<Expression<Func<CategoriaEntity, bool>>>()))
            .ReturnsAsync(categoriaExistente) // Primeiro GetAsync (CheckCategoria)
            .ReturnsAsync(categoriaDuplicada); // Segundo GetAsync (CheckNameExists)

        // Act
        Func<Task> act = async () => await _categoriaService.UpdateCategoria(id,request);

        // Assert
        await act.Should().ThrowAsync<KeyDuplicationException>();
    }
    
    [Fact(DisplayName = "Deve lançar exceção NotFoundException se a categoria não existir")]
    public async Task UpdateCategoria_Throws_NotFoundException()
    {
        // Arrange
        var id = 1;
        var request = new CategoriaRequest("Desodorante");

        _mockUof.Setup(u => u.CategoriaRepository.GetAsync(It.IsAny<Expression<Func<CategoriaEntity, bool>>>()))
            .ReturnsAsync(null as CategoriaEntity);

        // Act
        Func<Task> act = async () => await _categoriaService.UpdateCategoria(id, request);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Categoria não encontrada!");
    }
    
}