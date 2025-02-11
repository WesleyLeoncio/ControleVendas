using controle_vendas.modules.token.models.request;
using controle_vendas.modules.user.models.enums;
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
    [Route("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        return Ok(await _userService.Login(request));
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<RegisterResponse>> Register(UserRegisterRequest request)
    {
        return StatusCode(StatusCodes.Status201Created, 
            await _userService.Register(request));
    }

    [HttpPost]
    [Route("CreateRole")]
    public async Task<ActionResult<RegisterResponse>> CreateRole(Role role)
    {
        return StatusCode(StatusCodes.Status201Created, await _userService.CreateRole(role));
    }

    [HttpPost]
    [Route("AddUserToRole")]
    public async Task<ActionResult<RegisterResponse>> AddUserToRole(string userName, Role role)
    {
        return Ok(await _userService.AddUserToRole(userName, role));
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<ActionResult<RefreshTokenResponse>> RefreshToken(TokenRequest tokenRequest)
    {
        return Ok(await _userService.RefreshToken(tokenRequest));
    }


    [HttpPost]
    [Route("revoke/{userName}")]
    public async Task<IActionResult> Revoke(string userName)
    {
        await _userService.Revoke(userName);
        return NoContent();
    }
}