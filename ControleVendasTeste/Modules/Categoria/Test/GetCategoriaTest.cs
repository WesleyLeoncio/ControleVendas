using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Models.Mapper;
using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Categoria.Models.Response;
using ControleVendas.Modules.Categoria.Repository.Interfaces;
using ControleVendas.Modules.Categoria.Service;
using ControleVendas.Modules.Categoria.Service.Interfaces;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Produto.Models.Response;
using ControleVendasTeste.Config;
using ControleVendasTeste.Modules.Categoria.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using X.PagedList;
using X.PagedList.Extensions;

namespace ControleVendasTeste.Modules.Categoria.Test;

public class GetCategoriaTest
{
    private readonly ICategoriaService _categoriaService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public GetCategoriaTest()
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<ICategoriaRepository> mockCategoriaRepository = new Mock<ICategoriaRepository>();
        var mapper = AutoMapperConfig.Configure(new CategoriaMapper());

        _categoriaService = new CategoriaService(_mockUof.Object, mapper);
        _mockUof.Setup(u => u.CategoriaRepository).Returns(mockCategoriaRepository.Object);
    }

    [Fact(DisplayName = "Deve retornar CategoriaResponse ao buscar categoria por ID")]
    public async Task GetCategoriaById_Return_CategoriaResponse()
    {
        // Arrange
        _mockUof.Setup(u => u.CategoriaRepository.GetAsync(It.IsAny<Expression<Func<CategoriaEntity, bool>>>()))
            .ReturnsAsync(CategoriasData.GetCategoriaIndex(0));
        var categoriaId = 1;

        // Act
        CategoriaResponse act = await _categoriaService.GetCategoriaById(categoriaId);

        // Assert
        act.Should().NotBeNull();
        act.Should().BeOfType<CategoriaResponse>();
        act.Id.Should().Be(1);
        act.Nome.Should().Be("Perfume");
    }

    [Fact(DisplayName = "Deve retornar NotFoundException ao buscar categoria por ID inexistente")]
    public async Task GetCategoriaById_Return_NotFoundException()
    {
        // Arrange
        _mockUof.Setup(u => u.CategoriaRepository.GetAsync(It.IsAny<Expression<Func<CategoriaEntity, bool>>>()))
            .ReturnsAsync(null as CategoriaEntity);
        var categoriaId = 10;

        // Act
        Func<Task> act = async () => await _categoriaService.GetCategoriaById(categoriaId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Categoria não encontrada!");
    }

    [Theory(DisplayName =
        "Deve testar se o metodo GetAllFilterCategorias está retornando as categorias com e sem filtro")]
    [MemberData(nameof(CategoriasData.CategoriasRequest), MemberType = typeof(CategoriasData))]
    public async Task GetAllFilterCategorias_Return_CategoriasComFiltro_E_SemFiltro(string filterCategorias)
    {
        // Arrange
        _mockUof.Setup(u =>
                u.CategoriaRepository.GetAllFilterPageableAsync(It.IsAny<CategoriaFiltroRequest>()))
            .ReturnsAsync((CategoriaFiltroRequest request) =>
            {
                List<CategoriaEntity> categoriaFiltradas = CategoriasData.GetListCategorias()
                    .Where(c => c.Nome != null && (string.IsNullOrEmpty(request.Nome) ||
                                                   c.Nome.Contains(request.Nome, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
                
                return categoriaFiltradas.ToPagedList(request.PageNumber, request.PageSize);
            });


        CategoriaFiltroRequest request = new CategoriaFiltroRequest { Nome = filterCategorias };

        // Act
        CategoriaPaginationResponse act = await _categoriaService.GetAllFilterCategorias(request);

        //Assert
        act.Should().NotBeNull();
        
        if (!string.IsNullOrEmpty(request.Nome))
        {
            if (act.Categorias.Any())
            {
                // ✅ Caso existam categorias que correspondem ao filtro, elas devem ser retornadas corretamente
                act.Categorias.Should().NotBeEmpty();
                act.Categorias.Should().OnlyContain(c =>
                    c.Nome.Contains(request.Nome, StringComparison.OrdinalIgnoreCase)
                );
            }
            else
            {
                // ✅ Caso nenhuma categoria corresponda ao filtro, deve retornar uma lista vazia
                act.Categorias.Should().BeEmpty();
            }
        }
        else
        {
            // ✅ Se não há filtro, retorna a lista completa
            act.Categorias.Should().HaveCount(CategoriasData.GetListCategorias().Count());
        }

    }
    
  
}