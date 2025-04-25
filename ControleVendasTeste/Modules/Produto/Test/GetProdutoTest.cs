using System.Linq.Expressions;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Common.UnitOfWork.Interfaces;
using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Request;
using ControleVendas.Modules.Produto.Models.Response;
using ControleVendas.Modules.Produto.Repository.Interfaces;
using ControleVendas.Modules.Produto.Service;
using ControleVendas.Modules.Produto.Service.Interfaces;
using ControleVendasTeste.Modules.Produto.Config;
using ControleVendasTeste.Modules.Produto.Filter.Custom;
using ControleVendasTeste.Modules.Produto.Filter.Interfaces;
using ControleVendasTeste.Modules.Produto.Models;
using FluentAssertions;
using Moq;
using X.PagedList.Extensions;

namespace ControleVendasTeste.Modules.Produto.Test;

public class GetProdutoTest : IClassFixture<ProdutoConfigTest>
{
    private readonly IProdutoService _produtoService;
    private readonly Mock<IUnitOfWork> _mockUof;

    public GetProdutoTest(ProdutoConfigTest produtoConfigTest)
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<IProdutoRepository> mockProdutoRepository = new Mock<IProdutoRepository>();
        _produtoService = new ProdutoService(_mockUof.Object, produtoConfigTest.Mapper);
        _mockUof.Setup(u => u.ProdutoRepository).Returns(mockProdutoRepository.Object); 
    }
    
    [Fact(DisplayName = "Deve retornar ProdutoResponse ao buscar produto por ID")]
    public async Task GetProdutoById_Throws_NotFoundException()
    {
        // Arrange
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<ProdutoEntity, bool>>>()))
            .ReturnsAsync(null as ProdutoEntity);
        var produtoId = 10;

        // Act
        Func<Task> act = async () => await _produtoService.GetProdutoById(produtoId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Produto não encontrado!");
    }

    [Fact(DisplayName = "Deve retornar um lista de produtos ao buscar produtos")]
    public async Task GetAllFilterProdutos_Returns_ProdutoPaginationResponse()
    {
        // Arrange
        _mockUof.Setup(u => u.ProdutoRepository.GetAllFilterPageableAsync(It.IsAny<ProdutoFiltroRequest>()))
            .ReturnsAsync((ProdutoFiltroRequest request) =>
            {
                List<ProdutoEntity> produtos = ProdutosData.GetListProdutos().ToList();
                return produtos.ToPagedList(request.PageNumber, request.PageSize);
            });

        ProdutoFiltroRequest request = new ProdutoFiltroRequest();

        // Act
        ProdutoPaginationResponse act = await _produtoService.GetAllFilterProdutos(request);
        
        // Assert
        act.Should().NotBeNull();
        act.Produtos.Should().HaveCount(ProdutosData.GetListProdutos().Count);
    }
    
    [Theory(DisplayName = "Deve testar os filtros e retornar uma lista de produtos")]
    [MemberData(nameof(ProdutosData.ProdutoFiltroRequestData), MemberType = typeof(ProdutosData))]
    public async Task GetAllFilterProdutos_Test_Filter(ProdutoFiltroRequest request, int quantidadeProduto)
    {
        // Arrange
        List<ProdutoEntity> produtosList = ProdutosData.GetListProdutos();

        IEnumerable<IFilterProdutoResultTest> filterResults = new List<IFilterProdutoResultTest>
        {
            new FilterProdutoEstoqueTest(),
            new FilterNameProdutoTest(),
            new FilterFornecedorProdutoTest(),
            new FilterCategoriaProdutoTest(),
            new FilterPrecoCriterioProdutoTest()
        };
        
        foreach (var filter in filterResults)
        {
            produtosList = filter.RunFilter(produtosList, request);
        }
        
        _mockUof.Setup(u => u.ProdutoRepository.GetAllFilterPageableAsync(It.IsAny<ProdutoFiltroRequest>()))
            .ReturnsAsync(() => produtosList.ToPagedList(request.PageNumber, request.PageSize));

        
        // Act
        ProdutoPaginationResponse act = await _produtoService.GetAllFilterProdutos(request);
        
        // Assert
        act.Should().NotBeNull();
        act.Produtos.Should().NotBeEmpty();
        
        if (!string.IsNullOrEmpty(request.Nome)) act.Produtos.Should().OnlyContain(p =>
            p.Nome.Contains(request.Nome, StringComparison.OrdinalIgnoreCase));
        
        act.Produtos.Should().HaveCount(quantidadeProduto);
    }
}