using controle_vendas.modules.pedido.models.request;
using controle_vendas.modules.pedido.service.interfaces;
using controle_vendas.modules.user.models.entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace controle_vendas.modules.pedido.models.controller;

[ApiController]
[Route("[controller]")]
public class PedidoController : ControllerBase
{
    private readonly IPedidoService _pedidoService;
    private readonly UserManager<ApplicationUser> _userManager;

    public PedidoController(IPedidoService pedidoService, UserManager<ApplicationUser> userManager)
    {
        _pedidoService = pedidoService;
        _userManager = userManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<string>> CadastroDePedido([FromBody] PedidoRequest request)
    {
       // var user = await _userManager.FindByEmailAsync(User.);
       // if (user == null)
       // {
       //     return Unauthorized("Usuário não encontrado no banco");
       // }
       Console.WriteLine(User.Claims);
       return "teste";
    }
}