using System.Linq.Expressions;
using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Models.Mapper;
using ControleVendas.Modules.Categoria.Repository.Interfaces;
using ControleVendas.Modules.Categoria.Service;
using ControleVendas.Modules.Categoria.Service.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendasTeste.Config;
using ControleVendasTeste.Modules.Categoria.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Categoria.Test;

public class DeleteCategoriaTest
{
    private readonly ICategoriaService _categoriaService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public DeleteCategoriaTest()
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<ICategoriaRepository> mockCategoriaRepository = new Mock<ICategoriaRepository>();
        var mapper = AutoMapperConfig.Configure(new List<Profile>()
        {
            new CategoriaMapper()
        });

        _categoriaService = new CategoriaService(_mockUof.Object, mapper);
        _mockUof.Setup(u => u.CategoriaRepository).Returns(mockCategoriaRepository.Object); 
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