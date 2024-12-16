using System.Text.Json;
using controle_vendas.infra.exceptions.custom;
using controle_vendas.infra.exceptions.interfaces;

namespace controle_vendas.infra.exceptions.handle;

public class HandleNotFound : IErrorResultTask
{
    public Task ValidarException(ErrorExceptionResult error)
    {
        if (error.ExceptionType == typeof(NotFoundException))
        {
            int status = 404;
            string result = JsonSerializer.Serialize(new { status, mensage = error.Mensage});
            error.Context.Response.StatusCode = status;
            return error.Context.Response.WriteAsync(result);
        }
        
        return Task.FromResult(false);
    }
}