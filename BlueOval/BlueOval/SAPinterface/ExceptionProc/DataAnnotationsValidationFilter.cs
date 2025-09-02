using System.ComponentModel.DataAnnotations;

namespace BlueOval.SAPinterface.ExceptionProc;

// Create a validation filter
public class DataAnnotationsValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        foreach (var argument in context.Arguments)
        {
            if (argument is null) continue;

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(argument);

            if (!Validator.TryValidateObject(
                argument,
                validationContext,
                validationResults,
                true))
            {
                var errors = validationResults
                    .SelectMany(vr => vr.MemberNames
                        .Select(mn => new { mn, vr.ErrorMessage }))
                    .GroupBy(x => x.mn)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray()
                    );

                return Results.ValidationProblem(errors);
            }
        }

        return await next(context);
    }
}

// Usage
//app.MapPost("/deliveries", (DeliveryDto delivery) =>
//{
//    // Only reached if validation passes
//    return Results.Created($"/deliveries/{delivery.Id}", delivery);
//})
//.AddEndpointFilter<DataAnnotationsValidationFilter>();