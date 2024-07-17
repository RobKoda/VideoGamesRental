using FluentAssertions;
using FluentValidation;

// ReSharper disable MemberCanBePrivate.Global - Extension method
// ReSharper disable UnusedMember.Global - Extension method

namespace VideoGamesRental.Application.Tests.Core;

public static class StringErrorValidationExtensions
{
    public static async Task ValidateMaxLength<T>(this IValidator<T> inValidator, Func<T> inGetValidType, Func<T, string, T> inAssignValue, int inMaxSize, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateMaxLengthShouldError(inGetValidType, inAssignValue, inMaxSize, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateMaxLengthShouldWorkWithMaxLength(inGetValidType, inAssignValue, inMaxSize);
    }
    
    private static async Task ValidateMaxLengthShouldError<T>(this IValidator<T> inValidator, Func<T> inGetValidType, Func<T, string, T> inAssignValue, int inMaxSize, string inPropertyName, string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, new string('*', inMaxSize + 1));
        
        // Act
        var result = await inValidator.ValidateAsync(request);
        
        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateMaxLengthShouldWorkWithMaxLength<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, string, T> inAssignValue, int inMaxSize)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, new string('*', inMaxSize));

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}