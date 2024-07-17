using FluentAssertions;
using FluentValidation;

// ReSharper disable MemberCanBePrivate.Global - Extension method
// ReSharper disable UnusedMember.Global - Extension method

namespace VideoGamesRental.Application.Tests.Core;

public static class IntErrorValidationExtensions
{
    public static async Task ValidateIntIsLowerOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, int, int, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateIntIsLowerOrEqual(inGetValidType, inAssignValue, 0, inPropertyName,
            inErrorMessage);
    
    public static async Task ValidateIntIsLowerOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, int, int, T> inAssignValue, int inIntToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateIntIsLowerOrEqualShouldError(inGetValidType, inAssignValue, inIntToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateIntIsLowerOrEqualShouldWorkWithEqualInt(inGetValidType, inAssignValue, inIntToCompare);
    }

    private static async Task ValidateIntIsLowerOrEqualShouldError<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, int, int, T> inAssignValue, int inIntToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inIntToCompare, inIntToCompare + 1);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateIntIsLowerOrEqualShouldWorkWithEqualInt<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, int, int, T> inAssignValue, int inIntToCompare)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inIntToCompare, inIntToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    public static async Task ValidateIntIsLower<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, int, int, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateIntIsLower(inGetValidType, inAssignValue, 0, inPropertyName,
            inErrorMessage);

    public static async Task ValidateIntIsLower<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
            Func<T, int, int, T> inAssignValue, int inIntToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateIntIsLowerShouldErrorIfEqual(inGetValidType, inAssignValue, inIntToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateIntIsLowerShouldErrorIfAfter(inGetValidType, inAssignValue, inIntToCompare, inPropertyName,
            inErrorMessage);
    }

    private static async Task ValidateIntIsLowerShouldErrorIfEqual<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, int, int, T> inAssignValue, int inIntToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inIntToCompare, inIntToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateIntIsLowerShouldErrorIfAfter<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, int, int, T> inAssignValue, int inIntToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inIntToCompare, inIntToCompare + 1);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }
    
    public static async Task ValidateIntIsGreaterOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, int, int, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateIntIsGreaterOrEqual(inGetValidType, inAssignValue, 0, inPropertyName,
            inErrorMessage);

    public static async Task ValidateIntIsGreaterOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
            Func<T, int, int, T> inAssignValue, int inIntToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateIntIsGreaterOrEqualShouldError(inGetValidType, inAssignValue, inIntToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateIntIsGreaterOrEqualShouldWorkWithEqualInt(inGetValidType, inAssignValue, inIntToCompare);
    }

    private static async Task ValidateIntIsGreaterOrEqualShouldError<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, int, int, T> inAssignValue, int inIntToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inIntToCompare, inIntToCompare - 1);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateIntIsGreaterOrEqualShouldWorkWithEqualInt<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, int, int, T> inAssignValue, int inIntToCompare)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inIntToCompare, inIntToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    public static async Task ValidateIntIsGreater<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, int, int, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateIntIsGreater(inGetValidType, inAssignValue, 0, inPropertyName,
            inErrorMessage);

    public static async Task ValidateIntIsGreater<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
            Func<T, int, int, T> inAssignValue, int inIntToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateIntIsGreaterShouldErrorIfEqual(inGetValidType, inAssignValue, inIntToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateIntIsGreaterShouldErrorIfBefore(inGetValidType, inAssignValue, inIntToCompare, inPropertyName,
            inErrorMessage);
    }

    private static async Task ValidateIntIsGreaterShouldErrorIfEqual<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, int, int, T> inAssignValue, int inIntToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inIntToCompare, inIntToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateIntIsGreaterShouldErrorIfBefore<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, int, int, T> inAssignValue, int inIntToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inIntToCompare, inIntToCompare - 1);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }
}