using controle_vendas.modules.token.models.request;
using controle_vendas.modules.user.models.enums;
using controle_vendas.modules.user.models.request;
using controle_vendas.modules.user.models.response;

namespace controle_vendas.modules.user.service.interfaces;

public interface IUserService
{
    Task<RegisterResponse> Register(UserRegisterRequest request);
    Task<RegisterResponse> CreateRole(Role role);
    Task<RegisterResponse> AddUserToRole(string name, Role role);
    Task<LoginResponse> Login(LoginRequest request);
    Task<RefreshTokenResponse> RefreshToken(TokenRequest tokenRequest);
    Task Revoke(string name);
}