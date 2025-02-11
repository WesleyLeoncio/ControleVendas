using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using controle_vendas.infra.exceptions.custom;
using controle_vendas.modules.token.models.request;
using controle_vendas.modules.token.service.interfaces;
using controle_vendas.modules.user.models.entity;
using controle_vendas.modules.user.models.enums;
using controle_vendas.modules.user.models.request;
using controle_vendas.modules.user.models.response;
using controle_vendas.modules.user.service.interfaces;
using Microsoft.AspNetCore.Identity;

namespace controle_vendas.modules.user.service;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration, ITokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        ApplicationUser user = await CheckCredential(request);
        
        List<Claim> authClaims = await LoginUserAddRole(user);
        
        JwtSecurityToken token = _tokenService.GenerateAccessToken(authClaims,
            _configuration);

        string refreshToken = _tokenService.GenerateRefreshToken();

        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
            out int refreshTokenValidityInMinutes);

        user.RefreshTokenExpiryTime =
            DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

        user.RefreshToken = refreshToken;

        // Converter DateTime para UTC antes de salvar    
        user.RefreshTokenExpiryTime = user.RefreshTokenExpiryTime.ToUniversalTime();

        await _userManager.UpdateAsync(user);

        return new LoginResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken,
            token.ValidTo);
    }

    public async Task<RegisterResponse> Register(UserRegisterRequest request)
    {
       await CheckUserExists(request.Username);
        ApplicationUser user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) throw new ArgumentException("Ocorreram erros durante o registro!");
        await AddUserToRole(request.Username, Role.VENDEDOR);
        return new RegisterResponse("Success", "Usuario criado com sucesso!");
    }
    
    public async Task<RefreshTokenResponse> RefreshToken(TokenRequest tokenRequest)
    {
        tokenRequest.VerifyTokenRequest();
        
        var principal = _tokenService
            .GetPrincipalFromExpiredToken(tokenRequest.AccessToken ?? string.Empty, _configuration);
        
        string? username = principal.Identity?.Name;

        var user = await CheckRefreshToken(username ?? string.Empty,tokenRequest);
        
        var newAccessToken = _tokenService.GenerateAccessToken(
            principal.Claims.ToList(), _configuration);
        
        user.RefreshToken = _tokenService.GenerateRefreshToken();

        await _userManager.UpdateAsync(user);

        return new RefreshTokenResponse(
            new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            user.RefreshToken);
    }
    
    public async Task Revoke(string name)
    {
        ApplicationUser user = await CheckUserNameExists(name);
        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);
    }

    public async Task<RegisterResponse> CreateRole(Role role)
    {
        string roleName = role.ToString();
        await CheckRoleExists(roleName);
        var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if (!roleResult.Succeeded) throw new NotFoundException("Erro ao criar o role!");
        return new RegisterResponse("Success", "Role criada com sucesso!");
    }

    public async Task<RegisterResponse> AddUserToRole(string name, Role role)
    {
        ApplicationUser user = await CheckUserNameExists(name);
        string roleName = role.ToString();
        await CheckRoleNotExists(roleName);
        var roleResult = await _userManager.AddToRoleAsync(user, roleName);
        if (!roleResult.Succeeded) throw new NotFoundException("Erro ao tentar adicionar role!");
        return new RegisterResponse("Success", "Role adicionada com sucesso!");
    }

    private async Task CheckUserExists(string username)
    {
        ApplicationUser? appUser = await _userManager.FindByNameAsync(username);
        if (appUser != null) throw new KeyDuplicationException("Já existe um usuario com esse nome!");
    }
    
    private async Task<ApplicationUser> CheckRefreshToken(string username, TokenRequest tokenRequest)
    {
        ApplicationUser user = await CheckUserNameExists(username);

        if (user.RefreshToken != tokenRequest.RefreshToken
            || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new NotFoundException("Invalido access token/refresh token");
        }
       
        return user;
    }

    private async Task CheckRoleExists(string roleName)
    {
        if (await _roleManager.RoleExistsAsync(roleName))
            throw new KeyDuplicationException("Já existe uma role com esse nome!");
    }

    private async Task CheckRoleNotExists(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName)) throw new NotFoundException("Role não encontrada!");
    }

    private async Task<ApplicationUser> CheckUserNameExists(string name)
    {
        return await _userManager.FindByNameAsync(name) ??
               throw new NotFoundException("Usuário não encontrado!");
    }

    private async Task<ApplicationUser> CheckCredential(LoginRequest request)
    {
        ApplicationUser user = await CheckUserNameExists(request.UserName);
        if (await _userManager.CheckPasswordAsync(user, request.Password)) return user;
        throw new UnauthorizedException("Password Incorreto!");
    }

    private async Task<List<Claim>> LoginUserAddRole(ApplicationUser user)
    {
        IList<string> userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {   
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        
        return authClaims;
    }
}