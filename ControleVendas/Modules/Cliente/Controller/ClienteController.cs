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
[Authorize(Roles = nameof(Role.Vendedor))]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }
    
    ///<summary>Cadastra Um Novo Cliente</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
    [HttpPost]
    public async Task<ActionResult<ClienteResponse>> CadastroDeCliente(ClienteRequest request)
    {
        ClienteResponse response = await _clienteService.CreateCliente(request);
        return CreatedAtAction(nameof(BuscarClientePorId), new { id = response.Id }, response);
    }
    
    /// <summary>Altera Um Cliente</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [HttpPut("{id}")]
    public async Task<ActionResult> AlterarCliente(int id, ClienteRequest request)
    {
        await _clienteService.UpdateCliente(id, request);
        return NoContent();
    }
    
    /// <summary>Altera O Status Do Cliente </summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [HttpPut("Status/{id}")]
    public async Task<ActionResult> AlterarStatusCliente(int id)
    {
        await _clienteService.AlterStatusCliente(id);
        return NoContent();
    }
    
    /// <summary>Delete Um Cliente</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ClienteResponse>> DeleteCliente(int id)
    { 
        return Ok(await _clienteService.DeleteCliente(id));
    }
    
    ///<summary>Busca Um Cliente Pelo Id</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteResponse>> BuscarClientePorId(int id)
    {
        return Ok(await _clienteService.GetClienteById(id));
    }
    
    ///<summary>Lista Clientes Utilizando Filtros</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [HttpGet("Filter/Pagination")]
    public async Task<ActionResult<IEnumerable<ClienteResponse>>> ListarClienteComFiltro([FromQuery] ClienteFiltroRequest filtroRequest)
    {
        ClientePaginationResponse response = await _clienteService.GetAllFilterClientes(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Clientes);
    }
}