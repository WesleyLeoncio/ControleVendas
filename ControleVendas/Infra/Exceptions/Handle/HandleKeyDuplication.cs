using System.Text.Json;
using ControleVendas.Infra.Exceptions.custom;
using ControleVendas.Infra.Exceptions.interfaces;

namespace ControleVendas.Infra.Exceptions.handle;

public class HandleKeyDuplication : IErrorResultTask
{
    public Task ValidarException(ErrorExceptionResult error)
    {
        if (error.Exception.GetType() == typeof(KeyDuplicationException))
        {
            int status = 409;
            string result = JsonSerializer.Serialize(new { status, mensage = error.Message});
            error.Context.Response.StatusCode = status;
            return error.Context.Response.WriteAsync(result);
        }
        
        return Task.FromResult(false);
    }
}