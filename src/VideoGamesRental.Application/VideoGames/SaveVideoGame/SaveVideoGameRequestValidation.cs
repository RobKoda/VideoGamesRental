using FluentValidation;
using FluentValidation.Results;

namespace VideoGamesRental.Application.VideoGames.SaveVideoGame;

public class SaveVideoGameRequestValidation: AbstractValidator<SaveVideoGameRequest>
{
    public override async Task<ValidationResult> ValidateAsync(ValidationContext<SaveVideoGameRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateName();
        return await base.ValidateAsync(inContext, inCancellation);
    }

    private void ValidateName() =>
        ValidateNameLength();

    private void ValidateNameLength() =>
        RuleFor(inRequest => inRequest.VideoGame.Name)
            .Must(inLastName => inLastName.Length <= 128)
            .WithMessage(SaveVideoGameRequestValidationErrors.NameTooLong);
}