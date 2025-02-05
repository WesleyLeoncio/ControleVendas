namespace controle_vendas.infra.exceptions.custom;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(String msg) : base(msg)
    {
    }
}