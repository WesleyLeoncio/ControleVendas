using System.Linq.Expressions;
using AutoMapper;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Models.Mapper;
using ControleVendas.Modules.Fornecedor.Models.Request;
using ControleVendas.Modules.Fornecedor.Models.Response;
using ControleVendas.Modules.Fornecedor.Repository.Interfaces;
using ControleVendas.Modules.Fornecedor.Service;
using ControleVendas.Modules.Fornecedor.Service.Interfaces;
using ControleVendasTeste.Config;
using ControleVendasTeste.Modules.Fornecedor.Models;
using FluentAssertions;
using Moq;
using X.PagedList.Extensions;

namespace ControleVendasTeste.Modules.Fornecedor.Test;

public class GetFornecedorTest
{
    private readonly IFornecedorService _fornecedorService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public GetFornecedorTest()
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
    
    [Fact(DisplayName = "Deve retornar FornecedorResponse ao buscar fornecedor por ID")]
    public async Task GetCategoriaById_Return_CategoriaResponse()
    {
        // Arrange
        _mockUof.Setup(u => u.FornecedorRepository.GetAsync(It.IsAny<Expression<Func<FornecedorEntity, bool>>>()))
            .ReturnsAsync(FornecedoresData.GetFornecedorIndex(0));
        var fornecedorId = 1;

        // Act
        FornecedorResponse act = await _fornecedorService.GetFornecedorById(fornecedorId);

        // Assert
        act.Should().NotBeNull();
        act.Should().BeOfType<FornecedorResponse>();
        act.Id.Should().Be(1);
        act.Nome.Should().Be("Natura");
    }
    
    [Fact(DisplayName = "Deve retornar NotFoundException ao buscar fornecedor por ID inexistente")]
    public async Task GetFornecedorById_Return_NotFoundException()
    {
        // Arrange
        _mockUof.Setup(u => u.FornecedorRepository.GetAsync(It.IsAny<Expression<Func<FornecedorEntity, bool>>>()))
            .ReturnsAsync(null as FornecedorEntity);
        var fornecedorId = 1;

        // Act
        Func<Task> act = async () => await _fornecedorService.GetFornecedorById(fornecedorId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Fornecedor não encontrado!");
    }
    
    [Theory(DisplayName =
        "Deve testar se o metodo GetAllFilterFornecedor está retornando os fornecedores com e sem filtro")]
    [MemberData(nameof(FornecedoresData.FornecedoresGetFilter), MemberType = typeof(FornecedoresData))]
    public async Task GetAllFilterFornecedor_Return_CategoriasComFiltro_E_SemFiltro(string filterFornecedor)
    {
        // Arrange
        _mockUof.Setup(u =>
                u.FornecedorRepository.GetAllFilterPageableAsync(It.IsAny<FornecedorFiltroRequest>()))
            .ReturnsAsync((FornecedorFiltroRequest request) =>
            {
                List<FornecedorEntity> fornecedoresFiltradas = FornecedoresData.GetListFornecedores()
                    .Where(f => f.Nome != null && (string.IsNullOrEmpty(request.Nome) ||
                                                   f.Nome.Contains(request.Nome, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                return fornecedoresFiltradas.ToPagedList(request.PageNumber, request.PageSize);
            });


        FornecedorFiltroRequest request = new FornecedorFiltroRequest { Nome = filterFornecedor };

        // Act
        FornecedorPaginationResponse act = await _fornecedorService.GetAllFilterFornecedor(request);

        //Assert
        act.Should().NotBeNull();

        if (!string.IsNullOrEmpty(request.Nome))
        {
            if (act.Fornecedores.Any())
            {
                // ✅ Caso existam fornecedores que correspondem ao filtro, elas devem ser retornadas corretamente
                act.Fornecedores.Should().NotBeEmpty();
                act.Fornecedores.Should().OnlyContain(f =>
                    f.Nome.Contains(request.Nome, StringComparison.OrdinalIgnoreCase)
                );
            }
            else
            {
                // ✅ Caso nenhum fornecedor corresponda ao filtro, deve retornar uma lista vazia
                act.Fornecedores.Should().BeEmpty();
            }
        }
        else
        {
            // ✅ Se não há filtro, retorna a lista completa
            act.Fornecedores.Should().HaveCount(FornecedoresData.GetListFornecedores().Count());
        }
    }
}