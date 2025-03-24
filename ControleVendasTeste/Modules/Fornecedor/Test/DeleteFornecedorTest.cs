using System.Linq.Expressions;
using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Models.Mapper;
using ControleVendas.Modules.Fornecedor.Repository.Interfaces;
using ControleVendas.Modules.Fornecedor.Service;
using ControleVendas.Modules.Fornecedor.Service.Interfaces;
using ControleVendasTeste.Config;
using ControleVendasTeste.Modules.Fornecedor.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Fornecedor.Test;

public class DeleteFornecedorTest
{
    private readonly IFornecedorService _fornecedorService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public DeleteFornecedorTest()
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<IFornecedorRepository> mockFornecedorRepository = new Mock<IFornecedorRepository>();
        var mapper = AutoMapperConfig.Configure(new List<Profile>()
        {
            new FornecedorMapper()
        });

        _fornecedorService = new FornecedorService(_mockUof.Object, mapper);
        _mockUof.Setup(u => u.FornecedorRepository).Returns(mockFornecedorRepository.Object);
    }
    [Fact(DisplayName = "Deve deletar um forncedor com sucesso")]
    public async Task DeleteFornecedor_Success()
    {
        // Arrange
        var id = 1;
        
        _mockUof.Setup(u => u.FornecedorRepository.GetAsync(It.IsAny<Expression<Func<FornecedorEntity, bool>>>()))
            .ReturnsAsync(FornecedoresData.GetFornecedorIndex(0));
    
        // Act
        await _fornecedorService.DeleteFornecedor(id);
    
        // Assert
        _mockUof.Verify(u => u.FornecedorRepository.Delete(It.IsAny<FornecedorEntity>()), Times.Once);
       
        _mockUof.Verify(u => u.Commit(), Times.Once);
    }
    
    [Fact(DisplayName = "Deve lançar exceção NotFoundException ao tentar " +
                        "deletar um fornecedor que não existe")]
    public async Task DeleteFornecedor_Throws_NotFoundException()
    {
        // Arrange
        var id = 11;
        _mockUof.Setup(u => u.FornecedorRepository.GetAsync(It.IsAny<Expression<Func<FornecedorEntity, bool>>>()))
            .ReturnsAsync(null as FornecedorEntity);
    
        // Act
        Func<Task> act = async () => await _fornecedorService.DeleteFornecedor(id);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Fornecedor não encontrado!");
    }
    
}