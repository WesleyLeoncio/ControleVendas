using ControleVendas.Modules.Categoria.Models.Entity;
using ControleVendas.Modules.Categoria.Models.Request;

namespace ControleVendasTeste.Modules.Categoria.Models;

public static class CategoriasData
{
    public static IEnumerable<CategoriaEntity> GetListCategorias()
    {
        return new List<CategoriaEntity>
        {
            new CategoriaEntity { Id = 1, Nome = "Perfume" },
            new CategoriaEntity { Id = 2, Nome = "Desodorante" },
            new CategoriaEntity { Id = 3, Nome = "Creme" }
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
            new object[] { "Perfume" },
            new object[] { "Inexistente" },
            new object[] { "" },
        };
    }
}