using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ControleVendas.Modules.Token.Service.Interfaces;

public interface ITokenService
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration config);
    
    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration config);
}