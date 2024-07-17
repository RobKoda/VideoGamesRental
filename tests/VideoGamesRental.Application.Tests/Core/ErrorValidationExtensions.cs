using FluentAssertions;
using FluentValidation.Results;

// ReSharper disable MemberCanBePrivate.Global - Extension method
// ReSharper disable UnusedMember.Global - Extension method

namespace VideoGamesRental.Application.Tests.Core;

public static class ErrorValidationExtensions
{
    public static void ShouldError(this ValidationResult? inResult, string inPropertyName, string inSupposedError)
    {
        if (inResult == null) return;
        inResult.IsValid.Should().BeFalse();
        inResult.Errors.Count(inError =>
                inError.PropertyName == inPropertyName)
            .Should().Be(1);
        inResult.Errors.First().ErrorMessage.Should()
            .Be(inSupposedError);
    }
}