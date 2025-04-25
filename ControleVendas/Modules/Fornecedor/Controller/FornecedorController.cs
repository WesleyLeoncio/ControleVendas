using ControleVendas.Modules.Fornecedor.Models.Request;
using ControleVendas.Modules.Fornecedor.Models.Response;
using ControleVendas.Modules.Fornecedor.Service.Interfaces;
using ControleVendas.Modules.User.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ControleVendas.Modules.Fornecedor.Controller;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = nameof(Role.Vendedor))]
public class FornecedorController : ControllerBase
{
    private readonly IFornecedorService _fornecedorService;

    public FornecedorController(IFornecedorService fornecedorService)
    {
        _fornecedorService = fornecedorService;
    }
    
    ///<summary>Cadastra Um Novo Fornecedor</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
    [HttpPost]
    public async Task<ActionResult<FornecedorResponse>> CadastroDeFornecedor(FornecedorRequest request)
    {
        FornecedorResponse response = await _fornecedorService.CreateFornecedor(request);
        return CreatedAtAction(nameof(BuscarFornecedorPorId),
            new { id = response.Id }, response);
    }
    
    /// <summary>Altera Um Fornecedor</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [HttpPut("{id}")]
    public async Task<ActionResult> AlterarFornecedor(int id, FornecedorRequest request)
    {
        await _fornecedorService.UpdateFornecedor(id, request);
        return NoContent();
    }
    
    ///<summary>Busca Um Fornecedor Pelo ‘Id’</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [HttpGet("{id}")]
    public async Task<ActionResult<FornecedorResponse>> BuscarFornecedorPorId(int id)
    {
        return Ok(await _fornecedorService.GetFornecedorById(id));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<FornecedorResponse>> DeleteFornecedor(int id)
    {
        await _fornecedorService.DeleteFornecedor(id);
        return NoContent();
    }
    
    ///<summary>Lista Fornecedores Utilizando Filtros</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [HttpGet("Filter/Pagination")]
    public async Task<ActionResult<IEnumerable<FornecedorResponse>>> ListarFornecedorComFiltro(
        [FromQuery] FornecedorFiltroRequest filtroRequest)
    {
        FornecedorPaginationResponse response = await _fornecedorService.GetAllFilterFornecedor(filtroRequest);
        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Fornecedores);
    }
}