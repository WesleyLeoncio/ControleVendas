using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Service.Interfaces;
using ControleVendasTeste.Modules.Fornecedor.Config;
using ControleVendasTeste.Modules.Fornecedor.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Fornecedor.Test;

public class DeleteFornecedorTest : IClassFixture<FornecedorConfigTest>
{
    private readonly IFornecedorService _fornecedorService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public DeleteFornecedorTest(FornecedorConfigTest fornecedorConfigTest)
    {
        _fornecedorService = fornecedorConfigTest.FornecedorService;
        _mockUof = fornecedorConfigTest.MockUof;
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