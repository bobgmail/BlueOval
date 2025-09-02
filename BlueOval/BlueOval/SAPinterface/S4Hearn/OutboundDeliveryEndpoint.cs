using BlueOval.SAPinterface.Validation;
using BlueOval.SAPinterface.ExceptionProc;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace BlueOval.SAPinterface.S4Hearn;

public static class OutboundDeliveryEndpoint
{
    // Define an extension method for IEndpointRouteBuilder.
    public static IEndpointRouteBuilder MapOutboundDeliveryEndpoints(this IEndpointRouteBuilder app)
    {
        // Create an API group for user-related endpoints with a base route.
        var group = app.MapGroup("/OutboundDelivery")
                       //.RequireRateLimiting("fixed")
                       .WithTags("OutboundDelivery") // Optional: adds a tag for OpenAPI documentation.
                       .AddEndpointFilter<DeserializationExceptionFilter>();

        // Map the endpoints within the group.
        // group.MapGet("/", GetUsers);
        //group.MapGet("/{id}", GetUserById);
        group.MapPost("/", CreateShipmentDelievery)
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
    public static IResult CreateShipmentDelievery(OutboundDeliveryClass? delivery)
    {
        Return ret = new Return
        {
            returnCode = "S",
            returnDesc = "Information processed successfully"
        };
        if (delivery == null)
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
        var validationContext = new ValidationContext(delivery);
        // Important: Set validateAllProperties to true
        // This validates only the root object by default
        bool isValid = Validator.TryValidateObject(delivery, validationContext, validationResults, true);

        // Add this to validate nested objects
        ValidateUtility.ValidateNestedObjects(delivery, validationResults);

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
    

      
        if (delivery.A_OutbDeliveryHeader == null)
        {
            ret.returnCode = "E";
            ret.returnDesc = "A_OutbDeliveryHeader is required";
            return TypedResults.BadRequest(new
            {
                response = new Response
                {
                    Return = ret
                }
            });
        }
        else if (delivery.A_OutbDeliveryHeader.A_OutbDeliveryHeaderType == null)
        {
            ret.returnCode = "E";
            ret.returnDesc = "A_OutbDeliveryHeaderType is required";
            return TypedResults.BadRequest(new
            {
                response = new Response
                {
                    Return = ret
                }
            });
        }
        else if (delivery.A_OutbDeliveryHeader.A_OutbDeliveryHeaderType.to_DeliveryDocumentItem == null)
        {
            ret.returnCode = "E";
            ret.returnDesc = "to_DeliveryDocumentItem is required";
            return TypedResults.BadRequest(new
            {
                response = new Response
                {
                    Return = ret
                }
            });
        }
        else if (delivery.A_OutbDeliveryHeader.A_OutbDeliveryHeaderType.to_DeliveryDocumentItem.A_OutbDeliveryItemType == null
            || delivery.A_OutbDeliveryHeader.A_OutbDeliveryHeaderType.to_DeliveryDocumentItem.A_OutbDeliveryItemType.Length == 0)
        {
            ret.returnCode = "E";
            ret.returnDesc = "At least one Item is required";
            return TypedResults.BadRequest(new
            {
                response = new Response
                {
                    Return = ret
                }
            });
        }

        //var validationResults = new List<ValidationResult>();
        //var validationContext = new ValidationContext(delivery);

        //if (!Validator.TryValidateObject(delivery, validationContext, validationResults, true))
        //{
        //    // Validation failed, return a 400 Bad Request with a list of errors
        //    var errors = validationResults.ToDictionary(
        //        vr => vr.MemberNames.FirstOrDefault() ?? "Error",
        //        vr => vr.ErrorMessage!
        //    );
        //    return TypedResults.BadRequest(new { errors });
        //}
        // If we reach here, the payload is valid.
        //processing logic here

        //return "Creating a new user...";
        return TypedResults.Ok(new
            {
                response = new Response
                {
                    Return = ret
                }
            });
    }

   
  
   
};
    //public static string UpdateUser(int id) => $"Updating user with ID: {id}";
    // public static string DeleteUser(int id) => $"Deleting user with ID: {id}";
    

