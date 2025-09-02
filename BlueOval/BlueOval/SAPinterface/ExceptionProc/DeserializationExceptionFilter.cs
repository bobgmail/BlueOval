using System.Text.Json;

namespace BlueOval.SAPinterface.ExceptionProc;

public class DeserializationExceptionFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);
        }
        catch (BadHttpRequestException ex) when (ex.Message.Contains("JSON"))//(ex.InnerException is JsonException)
        {
            var jsonException = (JsonException)ex.InnerException;
            return Results.BadRequest(new { message = $"Invalid JSON format: {jsonException.Message}" });
        }
    }
}
