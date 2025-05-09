using ControleVendas.Modules.Cliente.Models.Entity;
using ControleVendas.Modules.Cliente.Models.Request;

namespace ControleVendasTeste.Modules.Cliente.Filter.Interfaces;

public interface IFilterClienteResultTest
{
    public List<ClienteEntity> RunFilter(List<ClienteEntity> clientes, ClienteFiltroRequest filtro);
}