using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Models.Request;
using ControleVendas.Modules.Fornecedor.Models.Response;
using ControleVendas.Modules.Fornecedor.Service.Interfaces;
using ControleVendasTeste.Modules.Fornecedor.Config;
using ControleVendasTeste.Modules.Fornecedor.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Fornecedor.Test;

public class PostFornecedorTest : IClassFixture<FornecedorConfigTest>
{
    private readonly IFornecedorService _fornecedorService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public PostFornecedorTest(FornecedorConfigTest fornecedorConfigTest)
    {
        _fornecedorService = fornecedorConfigTest.FornecedorService;
        _mockUof = fornecedorConfigTest.MockUof;
    }

    [Theory(DisplayName = "Deve testar se o metodo cadastro de fornecedor retorna FornecedorResponse")]
    [MemberData(nameof(FornecedoresData.FornecedoresRequest), MemberType = typeof(FornecedoresData))]
    public async Task PostFornecedor_Returns_FornecedorResponse(FornecedorRequest fornecedorRequest)
    {
        //Arrange
        _mockUof.Setup(u => u.FornecedorRepository.Create(It.IsAny<FornecedorEntity>()))
            .Returns((FornecedorEntity f) => f);
        // Configura o Commit para não fazer nada (simulação)
        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);

        //Act
        FornecedorResponse act = await _fornecedorService.CreateFornecedor(fornecedorRequest);

        //Assert
        act.Should().NotBeNull();
        act.Should().BeOfType<FornecedorResponse>();
        act.Nome.Should().Be(fornecedorRequest.Nome);
    }
    
    [Theory(DisplayName = "Deve testar se o metodo cadastro de fornecedor " +
                          "lança uma exeption KeyDuplicationException ao tentar cadastrar " +
                          "fornecedor com o mesmo nome")]
    [InlineData("Natura")]
    public async Task PostFornecedor_Throws_KeyDuplicationException(string nome)
    {
        //Arrange
        _mockUof.Setup(u => u.FornecedorRepository.Create(It.IsAny<FornecedorEntity>()))
            .Returns((FornecedorEntity f) => f);
        _mockUof.Setup(u => u.FornecedorRepository.GetAsync(It.IsAny<Expression<Func<FornecedorEntity, bool>>>()))
            .ReturnsAsync(FornecedoresData.GetFornecedorIndex(0));
        // Configura o Commit para não fazer nada (simulação)
        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);
        
        //Act
        Func<Task> act = async () => await _fornecedorService.CreateFornecedor(new FornecedorRequest(nome));
        
        //Assert
        await act.Should().ThrowAsync<KeyDuplicationException>();
        
    }
    
}

 
