namespace ControleVendas.Modules.User.Models.Response;

public record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken
);