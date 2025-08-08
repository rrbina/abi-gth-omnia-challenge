using FluentValidation.Results;

namespace DeveloperStore.Sales.Application.Helpers;

public static class ValidationMessageHelper
{
    public static string CreateMessageFromFailures(IEnumerable<ValidationFailure> failures)
    {
        return string.Join(" | ", failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}"));
    }
}