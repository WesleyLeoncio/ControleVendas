using System.Text.Json;
using controle_vendas.infra.exceptions.custom;
using controle_vendas.infra.exceptions.interfaces;

namespace controle_vendas.infra.exceptions.handle;

public class HandleKeyDuplication : IErrorResultTask
{
    public Task ValidarException(ErrorExceptionResult error)
    {
        if (error.Exception.GetType() == typeof(KeyDuplicationException))
        {
            Console.WriteLine("teste 1234");
            int status = 409;
            string result = JsonSerializer.Serialize(new { status, mensage = error.Mensage});
            error.Context.Response.StatusCode = status;
            return error.Context.Response.WriteAsync(result);
        }
        
        return Task.FromResult(false);
    }
}