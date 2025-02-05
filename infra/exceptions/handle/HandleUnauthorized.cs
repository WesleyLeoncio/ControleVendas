using System.Text.Json;
using controle_vendas.infra.exceptions.custom;
using controle_vendas.infra.exceptions.interfaces;

namespace controle_vendas.infra.exceptions.handle;

public class HandleUnauthorized : IErrorResultTask
{
    public Task ValidarException(ErrorExceptionResult error)
    {
        if (error.Exception.GetType() == typeof(UnauthorizedException))
        {
            int status = 403;
            string result = JsonSerializer.Serialize(new { status, mensage = error.Mensage});
            error.Context.Response.StatusCode = status;
            return error.Context.Response.WriteAsync(result);
        }
        
        return Task.FromResult(false);
    }
}