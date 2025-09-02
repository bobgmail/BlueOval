using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BlueOval.SAPinterface.Validation;

public class ValidateObjectAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;

        var results = new List<ValidationResult>();
        var context = new ValidationContext(value, null, null);

        Validator.TryValidateObject(value, context, results, true);

        if (results.Count != 0)
        {
            var compositeResults = new CompositeValidationResult($"Validation for {validationContext.DisplayName} failed!");
            results.ForEach(compositeResults.AddResult);
            return compositeResults;
        }

        return ValidationResult.Success;
    }
}

public class ValidateEnumerableAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;

        var results = new List<ValidationResult>();
        var enumerable = value as IEnumerable;

        if (enumerable != null)
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                var itemResults = new List<ValidationResult>();
                var itemContext = new ValidationContext(item, null, null);
                Validator.TryValidateObject(item, itemContext, itemResults, true);

                if (itemResults.Count != 0)
                {
                    foreach (var validationResult in itemResults)
                    {
                        var memberNames = validationResult.MemberNames.Select(x => $"[{index}].{x}");
                        results.Add(new ValidationResult(validationResult.ErrorMessage, memberNames));
                    }
                }
                index++;
            }
        }

        if (results.Count != 0)
        {
            var compositeResults = new CompositeValidationResult($"Validation for {validationContext.DisplayName} failed!");
            results.ForEach(compositeResults.AddResult);
            return compositeResults;
        }

        return ValidationResult.Success;
    }
}

public class CompositeValidationResult : ValidationResult
{
    private readonly List<ValidationResult> _results = new List<ValidationResult>();

    public IEnumerable<ValidationResult> Results => _results;

    public CompositeValidationResult(string errorMessage) : base(errorMessage) { }
    public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }

    public void AddResult(ValidationResult validationResult)
    {
        _results.Add(validationResult);
    }
}
