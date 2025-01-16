namespace controle_vendas.infra.exceptions.custom;

public class KeyDuplicationException : Exception
{
    public KeyDuplicationException(String msg) : base(msg)
    {
    }
}