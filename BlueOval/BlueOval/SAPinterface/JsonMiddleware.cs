using System.Globalization;
using System.Text.Json;

namespace BlueOval.SAPinterface;

public class JsonMiddleware
{
    private readonly RequestDelegate _next;

    public JsonMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.ContentType?.StartsWith("application/json") == true)
        {
            var originalBody = context.Request.Body;
            var memoryStream = new MemoryStream();

            try
            {
                // Copy the request body so we can read it multiple times
                await context.Request.Body.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                context.Request.Body = memoryStream;

                // Attempt to parse JSON without model binding
                try
                {
                    using var jsonDoc = await JsonDocument.ParseAsync(memoryStream);    //Manually validate the JSON
                    // Reset position if parsing succeeds
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await _next(context);
                }
                catch (JsonException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        response = new Response
                        {
                            Return = new Return
                            {
                                returnCode = "E",
                                returnDesc = "Invalid JSON Payload!"
                            }
                        }

                    });
                    return;
                }
            }
            finally
            {
                // Restore the original stream
                context.Request.Body = originalBody;
                memoryStream.Dispose();
            }
        }
        else
        {
            await _next(context);
        }
    }
}


public static class RequestJsonMiddlewareExtensions
{
    public static IApplicationBuilder UseJsonMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JsonMiddleware>();
    }
}

//app.Use(async (context, next) =>
//{
//    // Only intercept requests with JSON content
//    if (context.Request.ContentType?.StartsWith("application/json") == true)
//    {
//        var originalBody = context.Request.Body;
//        var memoryStream = new MemoryStream();

//        try
//        {
//            // Copy the request body so we can read it multiple times
//            await context.Request.Body.CopyToAsync(memoryStream);
//            memoryStream.Seek(0, SeekOrigin.Begin);
//            context.Request.Body = memoryStream;

//            // Attempt to parse JSON without model binding
//            try
//            {
//                using var jsonDoc = await JsonDocument.ParseAsync(memoryStream);
//                // Reset position if parsing succeeds
//                memoryStream.Seek(0, SeekOrigin.Begin);
//                await next();
//            }
//            catch (JsonException)
//            {
//                context.Response.StatusCode = StatusCodes.Status400BadRequest;
//                await context.Response.WriteAsJsonAsync(new
//                {
//                    response = new Response
//                    {
//                        Return = new Return
//                        {
//                            returnCode = "E",
//                            returnDesc = "Invalid JSON Payload!"
//                        }
//                    }

//                });
//                return;
//            }
//        }
//        finally
//        {
//            // Restore the original stream
//            context.Request.Body = originalBody;
//            memoryStream.Dispose();
//        }
//    }
//    else
//    {
//        await next();
//    }
//});