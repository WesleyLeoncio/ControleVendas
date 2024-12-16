namespace controle_vendas.infra.exceptions.custom;

public class NotFoundException : Exception
{
    public NotFoundException(String msg) : base(msg){}
}