using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Cliente.Models.Response;
using ControleVendas.Modules.Cliente.Service.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendasTeste.Modules.Cliente.Config;
using ControleVendasTeste.Modules.Cliente.Filter.Custom;
using ControleVendasTeste.Modules.Cliente.Filter.Interfaces;
using ControleVendasTeste.Modules.Cliente.Models;
using FluentAssertions;
using Moq;
using X.PagedList.Extensions;

namespace ControleVendasTeste.Modules.Cliente.Test;

public class GetClienteTest : IClassFixture<ClienteConfigTest>
{
    private readonly IClienteService _clienteService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public GetClienteTest(ClienteConfigTest clienteConfigTest)
    {
        _clienteService = clienteConfigTest.ClienteService;
        _mockUof = clienteConfigTest.MockUof;
    }
    
    [Fact(DisplayName = "Deve retornar ClienteResponse ao buscar cliente por ID")]
    public async Task GetClienteById_Return_ClienteResponse()
    {
        // Arrange
        _mockUof.Setup(u => u.ClienteRepository.GetAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>()))
            .ReturnsAsync(ClienteData.GetClienteIndex(0));
        var clienteId = 1;

        // Act
        ClienteResponse act = await _clienteService.GetClienteById(clienteId);

        // Assert
        act.Should().NotBeNull();
        act.Should().BeOfType<ClienteResponse>();
        act.Id.Should().Be(1);
        act.Nome.Should().Be("Carlos");
    }
    
    [Fact(DisplayName = "Deve retornar NotFoundException ao buscar cliente por ID inexistente")]
    public async Task GetClienteById_Return_NotFoundException()
    {
        // Arrange
        _mockUof.Setup(u => u.ClienteRepository.GetAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>()))
            .ReturnsAsync(null as ClienteEntity);
        var clienteId = 10;

        // Act
        Func<Task> act = async () => await _clienteService.GetClienteById(clienteId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Cliente não encontrado!");
    }
    
    [Theory(DisplayName =
        "Deve testar se o metodo GetAllFilterClientes está retornando as clientes com e sem filtro")]
    [MemberData(nameof(ClienteData.ClienteGetFilterRequest), MemberType = typeof(ClienteData))]
    public async Task GetAllFilterClientes_Return_ClienteComFiltro_E_SemFiltro(ClienteFiltroRequest request, int quantidadeCliente)
    {
        // Arrange
        List<ClienteEntity> clientesList = ClienteData.GetListClientes();

        IEnumerable<IFilterClienteResultTest> filterResults = new List<IFilterClienteResultTest>
        {
            new FilterNameClienteTest(),
            new FilterAtivoClienteTest()
        };

        foreach (var filter in filterResults)
        {
            clientesList = filter.RunFilter(clientesList,request);
        }
        _mockUof.Setup(u => u.ClienteRepository.GetAllFilterPageableAsync(It.IsAny<ClienteFiltroRequest>()))
            .ReturnsAsync(() => clientesList.ToPagedList(request.PageNumber, request.PageSize));
       
        // Act
        ClientePaginationResponse act = await _clienteService.GetAllFilterClientes(request);

        // Assert
        act.Should().NotBeNull();
        act.Clientes.Should().NotBeNull();
        if (!string.IsNullOrEmpty(request.Nome)) act.Clientes.Should().OnlyContain(categoriaResponse =>
           categoriaResponse.Nome.Contains(request.Nome,StringComparison.OrdinalIgnoreCase));
        
        act.Clientes.Should().HaveCount(quantidadeCliente);
    }
}