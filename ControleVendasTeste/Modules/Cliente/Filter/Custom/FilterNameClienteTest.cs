using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendasTeste.Modules.Cliente.Filter.Interfaces;

namespace ControleVendasTeste.Modules.Cliente.Filter.Custom;

public class FilterNameClienteTest : IFilterClienteResultTest
{
    public List<ClienteEntity> RunFilter(List<ClienteEntity> clientes, ClienteFiltroRequest filtro)
    {
        if (string.IsNullOrEmpty(filtro.Nome))
            return clientes;

        return clientes
            .Where(c => c?.Nome != null &&
                        c.Nome.Contains(filtro.Nome, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}