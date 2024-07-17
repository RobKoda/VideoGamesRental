using FluentAssertions;
using FluentValidation;

// ReSharper disable MemberCanBePrivate.Global - Extension method
// ReSharper disable UnusedMember.Global - Extension method

namespace VideoGamesRental.Application.Tests.Core;

public static class DateTimeErrorValidationExtensions
{
    public static async Task ValidateDateIsLowerOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
            Func<T, DateTime, DateTime, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateDateIsLowerOrEqual(inGetValidType, inAssignValue, DateTime.Today, inPropertyName,
            inErrorMessage);

    public static async Task ValidateDateIsLowerOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateDateIsLowerOrEqualShouldError(inGetValidType, inAssignValue, inDateToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateDateIsLowerOrEqualShouldWorkWithEqualDate(inGetValidType, inAssignValue, inDateToCompare);
    }

    private static async Task ValidateDateIsLowerOrEqualShouldError<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDateToCompare, inDateToCompare.AddDays(1));

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateDateIsLowerOrEqualShouldWorkWithEqualDate<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDateToCompare, inDateToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    public static async Task ValidateDateIsLower<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, DateTime, DateTime, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateDateIsLower(inGetValidType, inAssignValue, DateTime.Today, inPropertyName,
            inErrorMessage);

    public static async Task ValidateDateIsLower<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateDateIsLowerShouldErrorIfEqual(inGetValidType, inAssignValue, inDateToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateDateIsLowerShouldErrorIfAfter(inGetValidType, inAssignValue, inDateToCompare, inPropertyName,
            inErrorMessage);
    }

    private static async Task ValidateDateIsLowerShouldErrorIfEqual<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDateToCompare, inDateToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateDateIsLowerShouldErrorIfAfter<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDateToCompare, inDateToCompare.AddDays(1));

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    public static async Task ValidateDateIsGreaterOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, DateTime, DateTime, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateDateIsGreaterOrEqual(inGetValidType, inAssignValue, DateTime.Today, inPropertyName,
            inErrorMessage);

    public static async Task ValidateDateIsGreaterOrEqual<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateDateIsGreaterOrEqualShouldError(inGetValidType, inAssignValue, inDateToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateDateIsGreaterOrEqualShouldWorkWithEqualDate(inGetValidType, inAssignValue, inDateToCompare);
    }

    private static async Task ValidateDateIsGreaterOrEqualShouldError<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDateToCompare, inDateToCompare.AddDays(-1));

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateDateIsGreaterOrEqualShouldWorkWithEqualDate<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDateToCompare, inDateToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    public static async Task ValidateDateIsGreater<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, DateTime, DateTime, T> inAssignValue, string inPropertyName, string inErrorMessage) =>
        await inValidator.ValidateDateIsGreater(inGetValidType, inAssignValue, DateTime.Today, inPropertyName,
            inErrorMessage);

    public static async Task ValidateDateIsGreater<T>(this IValidator<T> inValidator, Func<T> inGetValidType,
        Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare, string inPropertyName, string inErrorMessage)
    {
        await inValidator.ValidateDateIsGreaterShouldErrorIfEqual(inGetValidType, inAssignValue, inDateToCompare, inPropertyName,
            inErrorMessage);
        await inValidator.ValidateDateIsGreaterShouldErrorIfBefore(inGetValidType, inAssignValue, inDateToCompare, inPropertyName,
            inErrorMessage);
    }

    private static async Task ValidateDateIsGreaterShouldErrorIfEqual<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDateToCompare, inDateToCompare);

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }

    private static async Task ValidateDateIsGreaterShouldErrorIfBefore<T>(this IValidator<T> inValidator,
        Func<T> inGetValidType, Func<T, DateTime, DateTime, T> inAssignValue, DateTime inDateToCompare, string inPropertyName,
        string inErrorMessage)
    {
        // Arrange
        var request = inGetValidType();
        request = inAssignValue(request, inDateToCompare, inDateToCompare.AddDays(-1));

        // Act
        var result = await inValidator.ValidateAsync(request);

        // Assert
        result.ShouldError(inPropertyName, inErrorMessage);
    }
}