using MediatR;
using VideoGamesRental.Application.VideoGames.Persistence;
// ReSharper disable UnusedType.Global - Implicit use

namespace VideoGamesRental.Application.VideoGames.GetVideoGameById;

public class GetVideoGameByIdHandler(IVideoGamesRepository inVideoGamesRepository)
    : IRequestHandler<GetVideoGameByIdQuery, GetVideoGameByIdResponse>
{
    public async Task<GetVideoGameByIdResponse> Handle(GetVideoGameByIdQuery inRequest, CancellationToken inCancellationToken) => 
        new(await inVideoGamesRepository.GetVideoGameByIdAsync(inRequest.Id));
}