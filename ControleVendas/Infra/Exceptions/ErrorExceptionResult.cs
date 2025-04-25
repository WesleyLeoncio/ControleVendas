using System.Text.Json;

namespace ControleVendas.Infra.Exceptions;

public class ErrorExceptionResult
{
    public HttpContext Context { get; set; }
    public string Message { get; set; }
    public Exception Exception { get; set; }
    

    public ErrorExceptionResult(HttpContext context, Exception exception)
    {
        Context = context;
        Message = exception.Message;
        Exception = exception;
    }
    
    public Task GetResultPadrao(Exception exception)
    {
        string msg = exception.Message;
        int status = 500;
        string result = JsonSerializer.Serialize(new { status , mensage = msg});
        Context.Response.StatusCode = status;
        return Context.Response.WriteAsync(result);
    }
}