using System.Text;
using FluentValidation.Results;

namespace VideoGamesRental.Api.Core;

public static class FluentValidationExtensions
{
    public static string ValidationFailureMessage(this ValidationResult inValidationResult) =>
        new StringBuilder("ValidationError\n")
            .Append(string.Join(';', inValidationResult.Errors.Select(inValidationFailure => inValidationFailure.ErrorMessage)))
            .ToString();
}