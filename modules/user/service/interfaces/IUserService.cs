using controle_vendas.modules.user.models.request;
using controle_vendas.modules.user.models.response;

namespace controle_vendas.modules.user.service.interfaces;

public interface IUserService
{
    Task<RegisterResponse> Register(UserRegisterRequest request);
    Task<RegisterResponse> CreateRole(string roleName);
    Task<RegisterResponse> AddUserToRole(string email, string roleName);
}