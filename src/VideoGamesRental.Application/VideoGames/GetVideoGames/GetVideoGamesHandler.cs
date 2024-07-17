using MediatR;
using VideoGamesRental.Application.VideoGames.Persistence;
// ReSharper disable UnusedType.Global - Implicit use

namespace VideoGamesRental.Application.VideoGames.GetVideoGames;

public class GetVideoGamesHandler(IVideoGamesRepository inVideoGamesRepository) : IRequestHandler<GetVideoGamesQuery, GetVideoGamesResponse>
{
    public async Task<GetVideoGamesResponse> Handle(GetVideoGamesQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inVideoGamesRepository.GetVideoGamesAsync());
}