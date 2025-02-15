namespace ControleVendas.Infra.Exceptions.custom;

public class KeyDuplicationException : Exception
{
    public KeyDuplicationException(String msg) : base(msg)
    {
    }
}