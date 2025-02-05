using controle_vendas.infra.exceptions.custom;

namespace controle_vendas.modules.token.models.request;

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