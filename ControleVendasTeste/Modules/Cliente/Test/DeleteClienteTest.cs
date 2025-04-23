using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Service.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendasTeste.Modules.Cliente.Config;
using ControleVendasTeste.Modules.Cliente.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Cliente.Test;

public class DeleteClienteTest : IClassFixture<ClienteConfigTest>
{
    private readonly IClienteService _clienteService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public DeleteClienteTest(ClienteConfigTest clienteConfigTest)
    {
        _clienteService = clienteConfigTest.ClienteService;
        _mockUof = clienteConfigTest.MockUof;
    }
    
    [Fact(DisplayName = "Deve deletar um cliente com sucesso")]
    public async Task DeleteCliente_Success()
    {
        // Arrange
        var id = 1;
        
        _mockUof.Setup(u => u.ClienteRepository.GetAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>()))
            .ReturnsAsync(ClienteData.GetClienteIndex(0));
    
        // Act
        await _clienteService.DeleteCliente(id);
    
        // Assert
        _mockUof.Verify(u => u.ClienteRepository.Delete(It.IsAny<ClienteEntity>()), Times.Once);
       
        _mockUof.Verify(u => u.Commit(), Times.Once);
    }
    
    [Fact(DisplayName = "Deve lançar exceção NotFoundException ao tentar " +
                        "deletar um cliente que não existe")]
    public async Task DeleteCliente_Throws_NotFoundException()
    {
        // Arrange
        var id = 1;
        _mockUof.Setup(u => u.ClienteRepository.GetAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>()))
            .ReturnsAsync(null as ClienteEntity);
    
        // Act
        Func<Task> act = async () => await _clienteService.DeleteCliente(id);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Cliente não encontrado!");
    }
    
}