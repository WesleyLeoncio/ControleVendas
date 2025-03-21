using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Models.Request;
using ControleVendas.Modules.Produto.Models.Entity;

namespace ControleVendasTeste.Modules.Categoria.Models;

public static class CategoriasData
{
    public static IEnumerable<CategoriaEntity> GetListCategorias()
    {
        return new List<CategoriaEntity>
        {
            new CategoriaEntity { Id = 1,
                Nome = "Perfume",
                Produtos = new List<ProdutoEntity>()
                {
                    new ProdutoEntity{Id = 1, Nome = "Produto 1", CategoriaId = 1, },
                }
                
            },
            new CategoriaEntity { 
                Id = 2, 
                Nome = "Desodorante",
                Produtos = new List<ProdutoEntity>()
                {
                    new ProdutoEntity{Id = 2, Nome = "Produto 2", CategoriaId = 1, },
                }
            },
            new CategoriaEntity { 
                Id = 3,
                Nome = "Creme",
                Produtos = new List<ProdutoEntity>()
                {
                    new ProdutoEntity{Id = 3, Nome = "Produto 3", CategoriaId = 2, },
                }
            }
        };
    }

    public static CategoriaEntity GetCategoriaIndex(int index)
    {
        return GetListCategorias().ElementAt(index);
    }

    public static IEnumerable<object[]> CategoriasRequest()
    {
        return new List<object[]>
        {
            new object[] { new CategoriaRequest("Perfume") },
            new object[] { new CategoriaRequest("Creme") }
        };
    }

    public static IEnumerable<object[]> CategoriasGetFilter()
    {
        return new List<object[]>
        {
            new object[] { "Perfume" },
            new object[] { "Inexistente" },
            new object[] { "" },
        };
    }
}