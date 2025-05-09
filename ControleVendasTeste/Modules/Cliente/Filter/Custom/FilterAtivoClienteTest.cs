using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendasTeste.Modules.Cliente.Filter.Interfaces;

namespace ControleVendasTeste.Modules.Cliente.Filter.Custom;

public class FilterAtivoClienteTest : IFilterClienteResultTest
{
    public List<ClienteEntity> RunFilter(List<ClienteEntity> clientes, ClienteFiltroRequest filtro)
    {
        if (filtro.Ativo != null)
        {
            return clientes
              .Where(c => c.Ativo == filtro.Ativo).ToList();  
        }
        return clientes;
    }
}