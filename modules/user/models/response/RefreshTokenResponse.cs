namespace controle_vendas.modules.user.models.response;

public record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken
);