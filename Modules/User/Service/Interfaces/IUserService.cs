using ControleVendas.Modules.Token.Models.Request;
using ControleVendas.Modules.User.Models.Enums;
using ControleVendas.Modules.User.Models.Request;
using ControleVendas.Modules.User.Models.Response;

namespace ControleVendas.Modules.User.Service.Interfaces;

public interface IUserService
{
    Task<RegisterResponse> Register(UserRegisterRequest request);
    Task<RegisterResponse> CreateRole(Role role);
    Task<RegisterResponse> AddUserToRole(string name, Role role);
    Task<LoginResponse> Login(LoginRequest request);
    Task<RefreshTokenResponse> RefreshToken(TokenRequest tokenRequest);
    Task Revoke(string name);
}