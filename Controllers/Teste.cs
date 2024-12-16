using Microsoft.AspNetCore.Mvc;

namespace controle_vendas.Controllers;

[ApiController]
[Route("[controller]")]
public class Teste : ControllerBase
{
    [HttpGet]
    public IActionResult ListarTeste()
    {
        return Ok("Teste");
    }
}