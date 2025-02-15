using ControleVendas.Infra.Exceptions.custom;

namespace ControleVendas.Modules.Token.Models.Request;

public class TokenRequest
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }

    public void VerifyTokenRequest()
    {
        if (string.IsNullOrEmpty(AccessToken) || string.IsNullOrEmpty(RefreshToken))
        {
            throw new NotFoundException("TokenRequest invalido!");
        }
    }
}