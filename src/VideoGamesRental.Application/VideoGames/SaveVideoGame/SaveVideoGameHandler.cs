using MediatR;
using VideoGamesRental.Application.VideoGames.Persistence;

namespace VideoGamesRental.Application.VideoGames.SaveVideoGame;

public class SaveVideoGameHandler(IVideoGamesRepository inRepository) : IRequestHandler<SaveVideoGameCommand>
{
    public async Task Handle(SaveVideoGameCommand inRequest, CancellationToken inCancellationToken) => 
        await inRepository.SaveVideoGameAsync(inRequest.VideoGame);
}