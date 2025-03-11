namespace ControleVendas.Infra.Exceptions.interfaces;

public interface IErrorResultTask
{
    public Task ValidarException(ErrorExceptionResult error);
}