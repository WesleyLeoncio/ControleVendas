using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Models.Request;
using ControleVendas.Modules.Fornecedor.Service.Interfaces;
using ControleVendasTeste.Modules.Fornecedor.Config;
using ControleVendasTeste.Modules.Fornecedor.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Fornecedor.Test;

public class PutFornecedorTest : IClassFixture<FornecedorConfigTest>
{
    private readonly IFornecedorService _fornecedorService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public PutFornecedorTest(FornecedorConfigTest fornecedorConfigTest)
    {
        _fornecedorService = fornecedorConfigTest.FornecedorService;
        _mockUof = fornecedorConfigTest.MockUof;
    }
    
     [Fact(DisplayName = "Deve atualizar um fornecedor com sucesso")]
    public async Task UpdateFornecedor_Success()
    {
        // Arrange
        var id = 1;
        var request = new FornecedorRequest("Natura Alterado");
        var fornecedorExistente = FornecedoresData.GetFornecedorIndex(0);
        
        _mockUof.Setup(u => u.FornecedorRepository.GetAsync(It.IsAny<Expression<Func<FornecedorEntity, bool>>>()))
            .ReturnsAsync((Expression<Func<FornecedorEntity, bool>> predicate) =>
            {
                var compiledPredicate = predicate.Compile(); 
                return compiledPredicate(fornecedorExistente) ? fornecedorExistente : null;
            });
    
        // Act
        await _fornecedorService.UpdateFornecedor(id, request);
    
        // Assert
        _mockUof.Verify(u => u.FornecedorRepository.Update(It.IsAny<FornecedorEntity>()), Times.Once);
       
        _mockUof.Verify(u => u.Commit(), Times.Once);
    }
    
    [Fact(DisplayName = "Deve lançar exceção ao tentar atualizar com um nome já existente")]
    public async Task UpdateFornecedor_Throws_KeyDuplicationException()
    {
       // Arrange
        int id = 1;
        var request = new FornecedorRequest("Boticario");
        var fornecedorExistente = FornecedoresData.GetFornecedorIndex(0);
        var forncedorDuplicada = FornecedoresData.GetFornecedorIndex(1);
        
        _mockUof.SetupSequence(u => u.FornecedorRepository.GetAsync(It.IsAny<Expression<Func<FornecedorEntity, bool>>>()))
            .ReturnsAsync(fornecedorExistente) // Primeiro GetAsync (CheckFornecedor)
            .ReturnsAsync(forncedorDuplicada); // Segundo GetAsync (CheckNameExists)
    
        // Act
        Func<Task> act = async () => await _fornecedorService.UpdateFornecedor(id,request);
    
        // Assert
        await act.Should().ThrowAsync<KeyDuplicationException>();
    }
    
    [Fact(DisplayName = "Deve lançar exceção NotFoundException se o fornecedor não existir")]
    public async Task UpdateFornecedor_Throws_NotFoundException()
    {
        // Arrange
        var id = 1;
        var request = new FornecedorRequest("Boticario");
    
        _mockUof.Setup(u => u.FornecedorRepository.GetAsync(It.IsAny<Expression<Func<FornecedorEntity, bool>>>()))
            .ReturnsAsync(null as FornecedorEntity);
    
        // Act
        Func<Task> act = async () => await _fornecedorService.UpdateFornecedor(id, request);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Fornecedor não encontrado!");
    }
}