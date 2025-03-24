using ControleVendas.Modules.Fornecedor.Models.Entity;
using ControleVendas.Modules.Fornecedor.Models.Request;

namespace ControleVendasTeste.Modules.Fornecedor.Models;

public static class FornecedoresData
{
    public static IEnumerable<FornecedorEntity> GetListFornecedores()
    {
        return new List<FornecedorEntity>
        {
            new FornecedorEntity { Id = 1,
                Nome = "Natura"
            },
            new FornecedorEntity { 
                Id = 2, 
                Nome = "Boticario"
            },
            new FornecedorEntity { 
                Id = 3,
                Nome = "Racco"
            }
        };
    }
    
    public static FornecedorEntity GetFornecedorIndex(int index)
    {
        return GetListFornecedores().ElementAt(index);
    }
    
    public static IEnumerable<object[]> FornecedoresRequest()
    {
        return new List<object[]>
        {
            new object[] { new FornecedorRequest("Natura") },
            new object[] { new FornecedorRequest("Boticario") },
        };
    }
    
    public static IEnumerable<object[]> FornecedoresGetFilter()
    {
        return new List<object[]>
        {
            new object[] { "Natura" },
            new object[] { "Inexistente" },
            new object[] { "" },
        };
    }
}