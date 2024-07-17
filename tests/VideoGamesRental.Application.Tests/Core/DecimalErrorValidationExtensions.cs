using FluentAssertions;
using FluentValidation;

// ReSharper disable MemberCanBePrivate.Global - Extension method
// ReSharper disable UnusedMember.Global - Extension method

namespace VideoGamesRental.Application.Tests.Core;

public static class DecimalErrorValidationExtensions
{
    public static async Task ValidateDecimalIsLowerOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, decimal, decimal, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateDecimalIsLowerOrEqual(inGetValidType, inAssignValue, 0, inPropertyName,
            inErrorMessage);
    
    public static async Task ValidateDecimalIsLowerOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateDecimalIsLowerOrEqualShouldError(inGetValidType, inAssignValue, inDecimalToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateDecimalIsLowerOrEqualShouldWorkWithEqualDecimal(inGetValidType, inAssignValue, inDecimalToCompare);
    }

    private static async Task ValidateDecimalIsLowerOrEqualShouldError<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDecimalToCompare, inDecimalToCompare + 1);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateDecimalIsLowerOrEqualShouldWorkWithEqualDecimal<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDecimalToCompare, inDecimalToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    public static async Task ValidateDecimalIsLower<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, decimal, decimal, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateDecimalIsLower(inGetValidType, inAssignValue, 0, inPropertyName,
            inErrorMessage);

    public static async Task ValidateDecimalIsLower<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
            Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateDecimalIsLowerShouldErrorIfEqual(inGetValidType, inAssignValue, inDecimalToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateDecimalIsLowerShouldErrorIfAfter(inGetValidType, inAssignValue, inDecimalToCompare, inPropertyName,
            inErrorMessage);
    }

    private static async Task ValidateDecimalIsLowerShouldErrorIfEqual<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDecimalToCompare, inDecimalToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateDecimalIsLowerShouldErrorIfAfter<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDecimalToCompare, inDecimalToCompare + 1);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }
    
    public static async Task ValidateDecimalIsGreaterOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, decimal, decimal, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateDecimalIsGreaterOrEqual(inGetValidType, inAssignValue, 0, inPropertyName,
            inErrorMessage);

    public static async Task ValidateDecimalIsGreaterOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
            Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateDecimalIsGreaterOrEqualShouldError(inGetValidType, inAssignValue, inDecimalToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateDecimalIsGreaterOrEqualShouldWorkWithEqualDecimal(inGetValidType, inAssignValue, inDecimalToCompare);
    }

    private static async Task ValidateDecimalIsGreaterOrEqualShouldError<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDecimalToCompare, inDecimalToCompare - 1);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateDecimalIsGreaterOrEqualShouldWorkWithEqualDecimal<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDecimalToCompare, inDecimalToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    public static async Task ValidateDecimalIsGreater<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, decimal, decimal, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateDecimalIsGreater(inGetValidType, inAssignValue, 0, inPropertyName,
            inErrorMessage);

    public static async Task ValidateDecimalIsGreater<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
            Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateDecimalIsGreaterShouldErrorIfEqual(inGetValidType, inAssignValue, inDecimalToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateDecimalIsGreaterShouldErrorIfBefore(inGetValidType, inAssignValue, inDecimalToCompare, inPropertyName,
            inErrorMessage);
    }

    private static async Task ValidateDecimalIsGreaterShouldErrorIfEqual<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDecimalToCompare, inDecimalToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateDecimalIsGreaterShouldErrorIfBefore<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, decimal, decimal, T> inAssignValue, decimal inDecimalToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDecimalToCompare, inDecimalToCompare - 1);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }
}