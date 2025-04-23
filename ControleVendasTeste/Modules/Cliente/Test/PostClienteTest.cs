using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Cliente.Models.Response;
using ControleVendas.Modules.Cliente.Service.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendasTeste.Modules.Cliente.Config;
using ControleVendasTeste.Modules.Cliente.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Cliente.Test;

public class PostClienteTest : IClassFixture<ClienteConfigTest>
{
    private readonly IClienteService _clienteService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public PostClienteTest(ClienteConfigTest clienteConfigTest)
    {
        _clienteService = clienteConfigTest.ClienteService;
        _mockUof = clienteConfigTest.MockUof;
    }
    
    [Theory(DisplayName = "Deve testar se o metodo cadastro de cliente retorna ClienteResponse")]
    [MemberData(nameof(ClienteData.ClienteRequest), MemberType = typeof(ClienteData))]
    public async Task PostCliente_Returns_ClienteResponse(ClienteRequest clienteRequest)
    {
        //Arrange
        _mockUof.Setup(u => u.ClienteRepository.Create(It.IsAny<ClienteEntity>()))
            .Returns((ClienteEntity c) => c);
        // Configura o Commit para não fazer nada (simulação)
        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);
       
        //Act
        ClienteResponse act = await _clienteService.CreateCliente(clienteRequest);
        
        //Assert
        act.Should().NotBeNull();
        act.Should().BeOfType<ClienteResponse>();
        act.Nome.Should().Be(clienteRequest.Nome);  
        
    }
    
    [Fact(DisplayName = "Deve testar se o metodo cadastro de cliente " +
                          "lança uma exeption KeyDuplicationException ao tentar cadastrar " +
                          "cllientes com o mesmo telefone")]
    public async Task PostCliente_Throws_KeyDuplicationException()
    {
        //Arrange
        _mockUof.Setup(u => u.ClienteRepository.Create(It.IsAny<ClienteEntity>()))
            .Returns((ClienteEntity c) => c);
        _mockUof.Setup(u => u.ClienteRepository.GetAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>()))
            .ReturnsAsync(ClienteData.GetClienteIndex(0));
        // Configura o Commit para não fazer nada (simulação)
        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);

        ClienteRequest request =  new ClienteRequest("Cliente Teste", "teste@gmail.com", "123456789");
        
        //Act
        Func<Task> act = async () => await _clienteService.CreateCliente(request);
        
        //Assert
        await act.Should().ThrowAsync<KeyDuplicationException>();
        
    }
}