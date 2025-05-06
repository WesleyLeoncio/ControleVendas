using System.Text.Json;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Infra.Exceptions.interfaces;

namespace ControleVendas.Infra.Exceptions.handle;

public class HandleConflict : IErrorResultTask
{
    public Task ValidarException(ErrorExceptionResult error)
    {
        if (error.Exception.GetType() == typeof(ConflictException))
        {
            int status = 409;
            string result = JsonSerializer.Serialize(new { status, mensage = error.Message});
            error.Context.Response.StatusCode = status;
            return error.Context.Response.WriteAsync(result);
        }
        
        return Task.FromResult(false);
    }
}