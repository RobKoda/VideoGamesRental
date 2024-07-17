using FluentValidation;
using FluentValidation.Results;
using VideoGamesRental.Application.VideoGames.Persistence;

namespace VideoGamesRental.Application.VideoGames.DeleteVideoGame;

public class DeleteVideoGameRequestValidation(IVideoGamesRepository inRepository) : AbstractValidator<DeleteVideoGameRequest>
{
    public override async Task<ValidationResult> ValidateAsync(ValidationContext<DeleteVideoGameRequest> inContext,
        CancellationToken inCancellation = new())
    {
        ValidateId();
        ValidateCanBeDeleted();
        return await base.ValidateAsync(inContext, inCancellation);
    }
    
    private void ValidateId() =>
        RuleFor(inRequest => inRequest.Id)
            .MustAsync((inId, _) => inRepository.AnyVideoGameAsync(inId))
            .WithMessage(DeleteVideoGameRequestValidationErrors.VideoGameNotFound);
    
    private void ValidateCanBeDeleted() =>
        RuleFor(inRequest => inRequest.Id)
            .MustAsync((inId, _) => inRepository.CanVideoGameBeDeletedAsync(inId))
            .WithMessage(DeleteVideoGameRequestValidationErrors.VideoGameCannotBeDeleted);
}