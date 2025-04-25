using ControleVendas.Modules.Token.Models.Request;
using ControleVendas.Modules.User.Models.Enums;
using ControleVendas.Modules.User.Models.Request;
using ControleVendas.Modules.User.Models.Response;
using ControleVendas.Modules.User.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    
    ///<summary>Realiza Login Do Usuário</summary>
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        return Ok(await _userService.Login(request));
    }
    
    ///<summary>Cadastra Um Novo Usuário</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<RegisterResponse>> Register(UserRegisterRequest request)
    {
        return StatusCode(StatusCodes.Status201Created, 
            await _userService.Register(request));
    }
    
    ///<summary>Cadastra Uma Nova Role</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
    [HttpPost]
    [Authorize(Roles = nameof(Role.Master))]
    [Route("CreateRole")]
    public async Task<ActionResult<RegisterResponse>> CreateRole(Role role)
    {
        return StatusCode(StatusCodes.Status201Created, await _userService.CreateRole(role));
    }

    ///<summary>Adiciona Uma Nova Role Para Um Usuário</summary>
    [HttpPost]
    [Authorize(Roles = nameof(Role.Master))]
    [Route("AddUserToRole")]
    public async Task<ActionResult<RegisterResponse>> AddUserToRole(string userName, Role role)
    {
        return Ok(await _userService.AddUserToRole(userName, role));
    }
    
    ///<summary>Atualiza O ‘Token’ Do Usuário</summary>
    [HttpPost]
    [Authorize(Roles = nameof(Role.Master))]
    [Route("refresh-token")]
    public async Task<ActionResult<RefreshTokenResponse>> RefreshToken(TokenRequest tokenRequest)
    {
        return Ok(await _userService.RefreshToken(tokenRequest));
    }
    
    ///<summary>Remove O Token Do Usuário</summary>
    [HttpPost]
    [Authorize(Roles = nameof(Role.Master))]
    [Route("revoke/{userName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Revoke(string userName)
    {
        await _userService.Revoke(userName);
        return NoContent();
    }
}