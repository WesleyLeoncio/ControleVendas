namespace ControleVendas.Infra.Exceptions.custom;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(String msg) : base(msg)
    {
    }
}