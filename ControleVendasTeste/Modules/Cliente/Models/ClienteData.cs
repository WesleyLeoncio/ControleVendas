using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;

namespace ControleVendasTeste.Modules.Cliente.Models;

public static class ClienteData
{
    public static IEnumerable<ClienteEntity> GetListClientes()
    {
        return new List<ClienteEntity>
        {
            new ClienteEntity { Id = 1, Nome = "Carlos", Email = "carlos@gmail.com", Telefone = "123456789" },
            new ClienteEntity { Id = 2, Nome = "Neuza", Email = "neuza@gmail.com", Telefone = "987654321" },
            new ClienteEntity { Id = 3, Nome = "Leticia", Email = "leticia@gmail.com", Telefone = "876543219" },
        };
    }

    public static ClienteEntity GetClienteIndex(int index)
    {
        return GetListClientes().ElementAt(index);
    }

    public static IEnumerable<object[]> ClienteRequest()
    {
        return new List<object[]>
        {
            new object[] { new ClienteRequest("Carlos", "carlos@gmail.com", "123456789") },
            new object[] { new ClienteRequest("Neuza", "neuza@gmail.com", "987654321") },
        };
    }

    public static IEnumerable<object[]> ClienteGetFilter()
    {
        return new List<object[]>
        {
            new object[] { "Carlos" },
            new object[] { "Inexistente" },
            new object[] { "" },
        };
    }
}