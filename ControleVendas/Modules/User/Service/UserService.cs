﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Modules.Token.Models.Request;
using ControleVendas.Modules.Token.Service.Interfaces;
using ControleVendas.Modules.User.Models.Entity;
using ControleVendas.Modules.User.Models.Enums;
using ControleVendas.Modules.User.Models.Request;
using ControleVendas.Modules.User.Models.Response;
using ControleVendas.Modules.User.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ControleVendas.Modules.User.Service;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUserEntity> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public UserService(UserManager<ApplicationUserEntity> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration, ITokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        ApplicationUserEntity userEntity = await CheckCredential(request);
        
        List<Claim> authClaims = await LoginUserAddRole(userEntity);
        
        JwtSecurityToken token = _tokenService.GenerateAccessToken(authClaims,
            _configuration);

        string refreshToken = _tokenService.GenerateRefreshToken();

        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"],
            out int refreshTokenValidityInMinutes);

        userEntity.RefreshTokenExpiryTime =
            DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

        userEntity.RefreshToken = refreshToken;

        // Converter DateTime para UTC antes de salvar    
        userEntity.RefreshTokenExpiryTime = userEntity.RefreshTokenExpiryTime.ToUniversalTime();

        await _userManager.UpdateAsync(userEntity);

        return new LoginResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken,
            token.ValidTo);
    }

    public async Task<RegisterResponse> Register(UserRegisterRequest request)
    {
       await CheckUserExists(request.Username);
        ApplicationUserEntity userEntity = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber
        };
        var result = await _userManager.CreateAsync(userEntity, request.Password);
        if (!result.Succeeded) throw new ArgumentException("Ocorreram erros durante o registro!");
        await AddUserToRole(request.Username, Role.Vendedor);
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
        ApplicationUserEntity userEntity = await CheckUserNameExists(name);
        userEntity.RefreshToken = null;
        await _userManager.UpdateAsync(userEntity);
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
        ApplicationUserEntity userEntity = await CheckUserNameExists(name);
        string roleName = role.ToString();
        await CheckRoleNotExists(roleName);
        var roleResult = await _userManager.AddToRoleAsync(userEntity, roleName);
        if (!roleResult.Succeeded) throw new NotFoundException("Erro ao tentar adicionar role!");
        return new RegisterResponse("Success", "Role adicionada com sucesso!");
    }

    private async Task CheckUserExists(string username)
    {
        ApplicationUserEntity? appUser = await _userManager.FindByNameAsync(username);
        if (appUser != null) throw new KeyDuplicationException("Já existe um usuario com esse nome!");
    }
    
    private async Task<ApplicationUserEntity> CheckRefreshToken(string username, TokenRequest tokenRequest)
    {
        ApplicationUserEntity userEntity = await CheckUserNameExists(username);

        if (userEntity.RefreshToken != tokenRequest.RefreshToken
            || userEntity.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new NotFoundException("Invalido access token/refresh token");
        }
       
        return userEntity;
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

    private async Task<ApplicationUserEntity> CheckUserNameExists(string name)
    {
        return await _userManager.FindByNameAsync(name) ??
               throw new NotFoundException("Usuário não encontrado!");
    }

    private async Task<ApplicationUserEntity> CheckCredential(LoginRequest request)
    {
        ApplicationUserEntity userEntity = await CheckUserNameExists(request.UserName);
        if (await _userManager.CheckPasswordAsync(userEntity, request.Password)) return userEntity;
        throw new UnauthorizedException("Password Incorreto!");
    }

    private async Task<List<Claim>> LoginUserAddRole(ApplicationUserEntity userEntity)
    {
        IList<string> userRoles = await _userManager.GetRolesAsync(userEntity);
        var authClaims = new List<Claim>
        {   
            new Claim(JwtRegisteredClaimNames.Sub, userEntity.Id),
            new Claim(ClaimTypes.Name, userEntity.UserName ?? string.Empty),
            new Claim("full_name", userEntity.FullName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        
        return authClaims;
    }
}