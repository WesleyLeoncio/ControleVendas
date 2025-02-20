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
[Authorize(Roles = nameof(Role.VENDEDOR))]
public class FornecedorController : ControllerBase
{
    private readonly IFornecedorService _fornecedorService;

    public FornecedorController(IFornecedorService fornecedorService)
    {
        _fornecedorService = fornecedorService;
    }

    [HttpPost]
    public async Task<ActionResult<FornecedorResponse>> CadastroDeFornecedor(FornecedorRequest request)
    {
        FornecedorResponse response = await _fornecedorService.CreateFornecedor(request);
        return CreatedAtAction(nameof(BuscarFornecedorPorId), 
            new { id = response.Id }, response);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<FornecedorResponse>> AlterarFornecedor(int id, FornecedorRequest request)
    {
        return Ok(await _fornecedorService.UpdateFornecedor(id, request));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FornecedorResponse>> BuscarFornecedorPorId(int id)
    {
        return Ok(await _fornecedorService.GetFornecedorById(id));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<FornecedorResponse>> DeletarFornecedor(int id)
    { 
        return Ok(await _fornecedorService.DeleteFornecedor(id));
    }
    
    [HttpGet("Filter/Pagination")]
    public async Task<ActionResult<IEnumerable<FornecedorResponse>>> ListarFornecedorComFiltro([FromQuery] FornecedorFiltroRequest filtroRequest)
    {
        FornecedorPaginationResponse response = await _fornecedorService.GetAllFilterFornecedor(filtroRequest);
        Response.Headers.Append("X-Pagination",JsonConvert.SerializeObject(response.MetaData));
        return Ok(response.Fornecedores);
    }
}