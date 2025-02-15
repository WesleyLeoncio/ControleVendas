using ControleVendas.Modules.Token.Models.Request;
using ControleVendas.Modules.User.Models.Enums;
using ControleVendas.Modules.User.Models.Request;
using ControleVendas.Modules.User.Models.Response;
using ControleVendas.Modules.User.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleVendas.Modules.User.Controller;

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