using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace BlueOval.SAPinterface.Validation;

public static class ValidateUtility
{
    // Recursive validation method
    public static void ValidateNestedObjects(object obj, List<ValidationResult> results, string parentPath = "")
    {
        if (obj == null) return;

        foreach (var property in obj.GetType().GetProperties())
        {
            var value = property.GetValue(obj);
            var currentPath = string.IsNullOrEmpty(parentPath)
                ? property.Name
                : $"{parentPath}.{property.Name}";

            // Validate the property itself
            var context = new ValidationContext(obj) { MemberName = property.Name };
            var propertyResults = new List<ValidationResult>();
            if (!Validator.TryValidateProperty(value, context, propertyResults))
            {
                results.AddRange(propertyResults.Select(r =>
                    new ValidationResult(r.ErrorMessage, new[] { currentPath })));
            }

            // Handle nested objects (but not strings or value types)
            if (value != null && !value.GetType().IsValueType && value is not string)
            {
                // Handle collections/arrays
                if (value is IEnumerable enumerable && !(value is string))
                {
                    int index = 0;
                    foreach (var item in enumerable)
                    {
                        ValidateNestedObjects(item, results, $"{currentPath}[{index}]");
                        index++;
                    }
                }
                else // Regular nested object
                {
                    ValidateNestedObjects(value, results, currentPath);
                }
            }
        }
    }

    public static string GetValidationErrors(List<ValidationResult> results)
    {
        var sb = new StringBuilder();

        foreach (var result in results)
        {
            if (result is CompositeValidationResult composite)
            {
                sb.AppendLine(GetValidationErrors(composite.Results.ToList()));
            }
            else
            {
                sb.AppendLine($"{string.Join(",", result.MemberNames)}: {result.ErrorMessage}");
            }
        }
        if(sb.Length == 0)
        {
            //sb.AppendLine("No validation errors found.");
        }
       return sb.ToString().TrimEnd();
        
    }

    public static string ErrorsToJson(List<ValidationResult> results)
    {
        var errors = results
            .SelectMany(vr => vr.MemberNames.Select(mn => new { Property = mn, Error = vr.ErrorMessage }))
            .GroupBy(x => x.Property)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.Error).ToArray()
            );

        return JsonSerializer.Serialize(errors);
    }
}
