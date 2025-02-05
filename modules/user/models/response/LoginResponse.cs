using System.IdentityModel.Tokens.Jwt;

namespace controle_vendas.modules.user.models.response;

public record LoginResponse(
    string Token,
    string RefreshToken,
    DateTime Expiration
);