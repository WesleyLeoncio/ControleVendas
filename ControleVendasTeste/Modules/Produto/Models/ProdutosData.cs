using ControleVendas.Modules.Produto.Models.Entity;
using ControleVendas.Modules.Produto.Models.Enums;
using ControleVendas.Modules.Produto.Models.Request;

namespace ControleVendasTeste.Modules.Produto.Models;

public static class ProdutosData
{
    public static List<ProdutoEntity> GetListProdutos()
    {
        return new List<ProdutoEntity>
        {
            new ProdutoEntity
            {
                Id = 1, Nome = "Produto 1",
                CategoriaId = 1, FornecedorId = 2,
                ValorCompra = 20, ValorVenda = 25,
                Descricao = "Descrição do produto 1", Estoque = 20,
                DataCadastro = DateTime.Today
            },
            new ProdutoEntity
            {
                Id = 1, Nome = "Produto 2",
                CategoriaId = 1, FornecedorId = 1,
                ValorCompra = 220, ValorVenda = 265,
                Descricao = "Descrição do produto 2", Estoque = 20,
                DataCadastro = DateTime.Today
            },
            new ProdutoEntity
            {
                Id = 1, Nome = "Produto 3",
                CategoriaId = 1, FornecedorId = 2,
                ValorCompra = 100, ValorVenda = 125,
                Descricao = "Descrição do produto 3", Estoque = 20,
                DataCadastro = DateTime.Today
            }
        };
    }

    public static IEnumerable<object[]> ProdutoFiltroRequestData()
    {
        return new List<object[]>
        {
            new object[]
            {
                new ProdutoFiltroRequest 
                { Nome = "Produto 1"},1
            },
            new object[]
            {
                new ProdutoFiltroRequest 
                    { Categoria = 1,Fornecedor = 1},1
            },
            new object[]
            {
                new ProdutoFiltroRequest 
                    { Nome = "Produto 3", Categoria = 1,Fornecedor = 2},1
            },
            new object[]
            {
                new ProdutoFiltroRequest 
                    { Preco = 25, PrecoCriterio = Criterio.IGUAL},1
            },
            new object[]
            {
                new ProdutoFiltroRequest 
                    { Preco = 100, PrecoCriterio = Criterio.MAIOR},2
            },
            new object[]
            {
                new ProdutoFiltroRequest 
                    { Preco = 30, PrecoCriterio = Criterio.MENOR},1
            },
        };
    }

    public static ProdutoRequest GetProdutoRequest()
    {
        return new ProdutoRequest("Produto 1", 20, 25,
            "Descrição do produto 1", 20, 1, 2);
    }

    public static IEnumerable<object[]> ProdutosRequest()
    {
        return new List<object[]>
        {
            new object[]
            {
                new ProdutoRequest("Produto 1", 20, 25,
                    "Descrição do produto 1", 20, 2, 1)
            },
            new object[]
            {
                new ProdutoRequest("Produto 2", 220, 265,
                    "Descrição do produto 2", 20, 1, 2)
            },
        };
    }


    public static ProdutoEntity GetProdutoIndex(int index)
    {
        return GetListProdutos()[index];
    }
}