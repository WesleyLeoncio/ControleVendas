using controle_vendas.modules.user.models.request;
using controle_vendas.modules.user.models.response;
using controle_vendas.modules.user.service.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace controle_vendas.modules.user.controller;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<RegisterResponse>> Register(UserRegisterRequest request)
    {
        return StatusCode(StatusCodes.Status201Created, await _userService.Register(request));
    }

    [HttpPost]
    [Route("CreateRole")]
    public async Task<ActionResult<RegisterResponse>> CreateRole(string roleName)
    {
        return StatusCode(StatusCodes.Status201Created, await _userService.CreateRole(roleName));
    }

    [HttpPost]
    [Route("AddUserToRole")]
    public async Task<ActionResult<RegisterResponse>> AddUserToRole(string email, string roleName)
    {
        return Ok(await _userService.AddUserToRole(email, roleName));
    }
}