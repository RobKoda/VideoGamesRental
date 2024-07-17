using MediatR;
using VideoGamesRental.Application.VideoGames.Persistence;

namespace VideoGamesRental.Application.VideoGames.DeleteVideoGame;

public class DeleteVideoGameHandler(IVideoGamesRepository inRepository) : IRequestHandler<DeleteVideoGameCommand>
{
    public async Task Handle(DeleteVideoGameCommand inRequest, CancellationToken inCancellationToken) => 
        await inRepository.DeleteVideoGameAsync(inRequest.Id);
}