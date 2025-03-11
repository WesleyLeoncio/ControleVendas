namespace ControleVendas.Modules.User.Models.Response;

public record LoginResponse(
    string Token,
    string RefreshToken,
    DateTime Expiration
);