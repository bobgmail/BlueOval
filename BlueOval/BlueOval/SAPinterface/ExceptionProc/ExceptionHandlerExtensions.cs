namespace BlueOval.SAPinterface.ExceptionProc;

using Microsoft.AspNetCore.Diagnostics;

public static class ExceptionHandlerExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionHandler?.Error;

                if (exception is BadHttpRequestException badRequest)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        response = new Response
                        {
                            Return = new Return
                            {
                                returnCode = "E",
                                returnDesc = badRequest.Message// "Invalid JSON Payload!"
                            }
                        }

                    });

                }
                //var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                //var exception = exceptionHandlerPathFeature?.Error;
                //var response = new { error = exception?.Message ?? "An unknown error occurred." };

                //context.Response.ContentType = "application/json";
                //context.Response.StatusCode = 500;
                //await context.Response.WriteAsJsonAsync(response);
            });
        });
    }
}