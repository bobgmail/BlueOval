using BlueOval.SAPinterface.Validation;
using BlueOval.SAPinterface.ExceptionProc;
using System.ComponentModel.DataAnnotations;
using BlueOval.SAPinterface.S4Hearn;

namespace BlueOval.SAPinterface.S4Hearn;

public static class InspectionResultEndpoint
{
    public static IEndpointRouteBuilder MapInspectionResultEndpoints(this IEndpointRouteBuilder app)
    {
        // Create an API group for user-related endpoints with a base route.
        var group = app.MapGroup("/InspectionResult")
                       //.RequireRateLimiting("fixed")
                       .WithTags("InspectionResult") // Optional: adds a tag for OpenAPI documentation.
                       .AddEndpointFilter<DeserializationExceptionFilter>();

        // Map the endpoints within the group.
        // group.MapGet("/", GetUsers);
        //group.MapGet("/{id}", GetUserById);
        group.MapPost("/", CreateInspectionResult)
            .RequireAuthorization("BasicApiPolicy");

        //group.MapPut("/{id}", UpdateUser);
        //group.MapDelete("/{id}", DeleteUser);

        // Return the endpoint route builder for method chaining.
        return app;
    }

    // Handlers for the endpoints. These can be defined as local methods,
    // or more commonly, as separate service classes.
    //public static string GetUsers() => "Getting all users...";
    //public static string GetUserById(int id) => $"Getting user with ID: {id}";
    public static IResult CreateInspectionResult(InspectionResultPayload? inspectionResult)
    {
        Return ret = new Return
        {
            returnCode = "S",
            returnDesc = "Information processed successfully"
        };
        if (inspectionResult == null)
        {
            ret.returnCode = "E";
            ret.returnDesc = "Paylod is required";
            return TypedResults.BadRequest(new
            {
                response = new Response
                {
                    Return = ret
                }
            });
        }

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(inspectionResult);
        // Important: Set validateAllProperties to true
        // This validates only the root object by default
        bool isValid = Validator.TryValidateObject(inspectionResult, validationContext, validationResults, true);

        // Add this to validate nested objects
        ValidateUtility.ValidateNestedObjects(inspectionResult, validationResults);

        if (!isValid || validationResults.Any())
        {
            //var errors = validationResults
            //    .SelectMany(vr => vr.MemberNames
            //        .Select(mn => new { Property = mn, Error = vr.ErrorMessage }))
            //    .GroupBy(x => x.Property)
            //    .ToDictionary(
            //        g => g.Key,
            //        g => g.Select(x => x.Error).ToArray()
            //    );
            // Convert to formatted string
            string errorString = ValidateUtility.GetValidationErrors(validationResults);

            // Or convert to JSON
            //string errorJson = ErrorsToJson(validationResults);


            ret.returnCode = "E";
            ret.returnDesc = errorString;
            return TypedResults.BadRequest(new
            {
                response = new Response
                {
                    Return = ret
                }
            });
            //return Results.ValidationProblem(errors);
        }



        if (inspectionResult.InspectionLotUsageDecision == null)
        {
            ret.returnCode = "E";
            ret.returnDesc = "InspectionLotUsageDecision is required";
            return TypedResults.BadRequest(new
            {
                response = new Response
                {
                    Return = ret
                }
            });
        }
    

        //return "Creating a new user...";
        return TypedResults.Ok(new
        {
            response = new Response
            {
                Return = ret
            }
        });
    }



}


/*
 If InspectionLotUsageDecisionCode = “A1”, then the Lot Results is PASSED

Else Lot Result is FAILED

 

{
    "InspectionLotUsageDecision": {
        "Material": "MD-GAT-001-1315L",
        "Batch": "REINSP02",
        "BatchBySupplier": "SUPREIN02",
        "InspectionLotUsageDecisionCode": "A1"
    }
}
 */