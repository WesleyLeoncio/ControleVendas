namespace ControleVendas.Infra.Exceptions.custom;

public class NotFoundException : Exception
{
    public NotFoundException(String msg) : base(msg){}
}