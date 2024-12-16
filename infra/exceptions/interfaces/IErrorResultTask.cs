namespace controle_vendas.infra.exceptions.interfaces;

public interface IErrorResultTask
{
    public Task ValidarException(ErrorExceptionResult error);
}