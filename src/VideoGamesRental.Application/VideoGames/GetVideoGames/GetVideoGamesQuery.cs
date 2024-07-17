using MediatR;

namespace VideoGamesRental.Application.VideoGames.GetVideoGames;

public record GetVideoGamesQuery : IRequest<GetVideoGamesResponse>;