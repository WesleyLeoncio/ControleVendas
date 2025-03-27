using System.Linq.Expressions;
using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Mapper;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Cliente.Repository.Interfaces;
using ControleVendas.Modules.Cliente.Service;
using ControleVendas.Modules.Cliente.Service.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendasTeste.Config;
using ControleVendasTeste.Modules.Cliente.Models;
using FluentAssertions;
using Moq;

namespace ControleVendasTeste.Modules.Cliente.Test;

public class PutClienteTest
{
    private readonly IClienteService _clienteService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public PutClienteTest()
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<IClienteRepository> mockClienteRepository = new Mock<IClienteRepository>();
        var mapper = AutoMapperConfig.Configure(new List<Profile>()
        {
            new ClienteMapper()
        });

        _clienteService = new ClienteService(_mockUof.Object, mapper);
        _mockUof.Setup(u => u.ClienteRepository).Returns(mockClienteRepository.Object); 
    }
    
    [Fact(DisplayName = "Deve atualizar uma Cliente com sucesso")]
    public async Task UpdateCliente_Success()
    {
        // Arrange
        var id = 1;
        ClienteRequest request = 
            new ClienteRequest("Cliente Editado", "teste@gmail.com", "123456782");
        var clienteExistente = ClienteData.GetClienteIndex(0);
        
        _mockUof.Setup(u => u.ClienteRepository.GetAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>()))
            .ReturnsAsync((Expression<Func<ClienteEntity, bool>> predicate) =>
            {
                var compiledPredicate = predicate.Compile(); 
                return compiledPredicate(clienteExistente) ? clienteExistente : null;
            });
    
        // Act
        await _clienteService.UpdateCliente(id, request);
    
        // Assert
        _mockUof.Verify(u => u.ClienteRepository.Update(It.IsAny<ClienteEntity>()), Times.Once);
       
        _mockUof.Verify(u => u.Commit(), Times.Once);
    }
    
    [Fact(DisplayName = "Deve lançar exceção ao tentar atualizar com um telefone já existente")]
    public async Task UpdateCliente_Throws_KeyDuplicationException()
    {
        // Arrange
        int id = 1;
        ClienteRequest request = 
            new ClienteRequest("Cliente Editado", "teste@gmail.com", "987654321");
        var clienteExistente = ClienteData.GetClienteIndex(0);
        var clienteDuplicada = ClienteData.GetClienteIndex(1);
        
        _mockUof.SetupSequence(u => u.ClienteRepository.GetAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>()))
            .ReturnsAsync(clienteExistente) // Primeiro GetAsync (CheckCliente)
            .ReturnsAsync(clienteDuplicada); // Segundo GetAsync (CheckTelefoneExists)

        // Act
        Func<Task> act = async () => await _clienteService.UpdateCliente(id,request);

        // Assert
        await act.Should().ThrowAsync<KeyDuplicationException>();
    }
    
    [Fact(DisplayName = "Deve lançar exceção NotFoundException se o cliente não existir")]
    public async Task UpdateCliente_Throws_NotFoundException()
    {
        // Arrange
        var id = 1;
        ClienteRequest request = 
            new ClienteRequest("Cliente Editado", "teste@gmail.com", "123456782");

        _mockUof.Setup(u => u.ClienteRepository.GetAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>()))
            .ReturnsAsync(null as ClienteEntity);

        // Act
        Func<Task> act = async () => await _clienteService.UpdateCliente(id, request);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Cliente não encontrado!");
    }
}