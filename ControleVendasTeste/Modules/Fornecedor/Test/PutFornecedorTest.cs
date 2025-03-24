using System.Linq.Expressions;
using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Models.Mapper;
using ControleVendas.Modules.Fornecedor.Models.Request;
using ControleVendas.Modules.Fornecedor.Repository.Interfaces;
using ControleVendas.Modules.Fornecedor.Service;
using ControleVendas.Modules.Fornecedor.Service.Interfaces;
using ControleVendasTeste.Config;
using ControleVendasTeste.Modules.Fornecedor.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Fornecedor.Test;

public class PutFornecedorTest
{
    private readonly IFornecedorService _fornecedorService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public PutFornecedorTest()
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