using controle_vendas.infra.exceptions.custom;
using controle_vendas.modules.user.models.entity;
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

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }


    public async Task<RegisterResponse> Register(UserRegisterRequest request)
    {
        await CheckUserExists(request.Username);
        ApplicationUser user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username,
            PhoneNumber = request.PhoneNumber
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        await AddUserToRole(request.Email, "VENDEDOR");
        if (!result.Succeeded) throw new ArgumentException("Ocorreram erros durante o registro!");
        return new RegisterResponse("Success", "Usuario criado com sucesso!");
    }

    public async Task<RegisterResponse> CreateRole(string roleName)
    {
        await CheckRoleExists(roleName);
        var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if (!roleResult.Succeeded) throw new NotFoundException("Erro ao criar o role!");
        return new RegisterResponse("Success", "Role criada com sucesso!");
    }

    public async Task<RegisterResponse> AddUserToRole(string email, string roleName)
    {
        ApplicationUser user = await CheckUserEmailExists(email);
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
    private async Task CheckRoleExists(string roleName)
    {
        if (await _roleManager.RoleExistsAsync(roleName)) throw new KeyDuplicationException("Já existe uma role com esse nome!");
    }
    private async Task CheckRoleNotExists(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName)) throw new NotFoundException("Role não encontrada!");
    }
    private async Task<ApplicationUser> CheckUserEmailExists(string email)
    {
        return await _userManager.FindByEmailAsync(email) ?? 
               throw new NotFoundException("Usuário não encontrado!");
    }
    
}