using System.Text.Json;

namespace BlueOval.SAPinterface.ExceptionProc;

public class JsonExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public JsonExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (JsonException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { message = $"Invalid JSON format: {ex.Message}" });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = "An unexpected error occurred." });
        }
    }
}