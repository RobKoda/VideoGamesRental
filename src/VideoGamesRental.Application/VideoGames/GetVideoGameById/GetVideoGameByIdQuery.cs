using MediatR;

namespace VideoGamesRental.Application.VideoGames.GetVideoGameById;

public record GetVideoGameByIdQuery(Guid Id) : IRequest<GetVideoGameByIdResponse>;