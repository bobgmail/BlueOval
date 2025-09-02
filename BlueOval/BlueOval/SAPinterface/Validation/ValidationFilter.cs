using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BlueOval.SAPinterface.Validation;

public class ValidationFilterx<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator is not null)
        {
            var argToValidate = context.Arguments
                .OfType<T>()
                .FirstOrDefault();

            if (argToValidate is not null)
            {
                var result = await validator.ValidateAsync(argToValidate);
                if (!result.IsValid)
                {
                    return Results.ValidationProblem(result.ToDictionary());
                }
            }
        }

        return await next(context);
    }
}
public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var arg = context.Arguments
            .OfType<T>()
            .FirstOrDefault();

        if (arg == null)
        {
            return Results.Problem(
                statusCode: 400,
                title: "Invalid request",
                detail: "Request body is required");
        }

        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(
            arg,
            new ValidationContext(arg),
            validationResults,
            true);

        if (!isValid)
        {
            return Results.ValidationProblem(
                validationResults.ToDictionary(
                    v => v.MemberNames.FirstOrDefault() ?? "General",
                    v => new[] { v.ErrorMessage ?? "Invalid value" }
                ),
                statusCode: 400,
                title: "Validation failed"
            );
        }

        return await next(context);
    }
}


// Use in endpoint filter
public class FluentValidationFilter<T> : IEndpointFilter
{
    private readonly IValidator<T> _validator;

    public FluentValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var arg = context.Arguments.OfType<T>().FirstOrDefault();
        if (arg == null)
        {
            return Results.Problem("Request body is required", statusCode: 400);
        }

        var validationResult = await _validator.ValidateAsync(arg);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(
                validationResult.ToDictionary(),
                statusCode: 400,
                title: "Validation failed"
            );
        }

        return await next(context);
    }
}
// Usage
//app.MapPost("/orders", (OrderDto order) => Results.Ok(order))
//    .AddEndpointFilter<ValidationFilter<OrderDto>>();