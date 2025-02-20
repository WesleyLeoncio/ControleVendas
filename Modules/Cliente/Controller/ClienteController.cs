using ControleVendas.Modules.Cliente.Models.Request;
using ControleVendas.Modules.Cliente.Models.Response;
using ControleVendas.Modules.Cliente.Service.Interfaces;
using ControleVendas.Modules.User.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleVendas.Modules.Cliente.Controller;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = nameof(Role.VENDEDOR))]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpPost]
    public async Task<ActionResult<ClienteResponse>> CadastroDeCliente(ClienteRequest request)
    {
        ClienteResponse response = await _clienteService.CreateCliente(request);
        return CreatedAtAction(nameof(BuscarClientePorId), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ClienteResponse>> AlterarCliente(int id, ClienteRequest request)
    {
        return Ok(await _clienteService.UpdateCliente(id, request));
    }

    [HttpPut("Status/{id}")]
    public async Task<ActionResult<String>> AlterarStatusCliente(int id)
    {
        return Ok(await _clienteService.AlterStatusCliente(id));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ClienteResponse>> DeletarCliente(int id)
    { 
        return Ok(await _clienteService.DeleteCliente(id));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteResponse>> BuscarClientePorId(int id)
    {
        return Ok(await _clienteService.GetClienteById(id));
    }
    
    [HttpGet("Filter/Pagination")]
    public async Task<ActionResult<IEnumerable<ClienteResponse>>> ListarClienteComFiltro([FromQuery] ClienteFiltroRequest filtroRequest)
    {
        ClientePaginationResponse response = await _clienteService.GetAllFilterClientes(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Clientes);
    }
}