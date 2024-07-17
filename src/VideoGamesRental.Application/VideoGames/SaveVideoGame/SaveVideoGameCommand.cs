using MediatR;
using VideoGamesRental.Domain.VideoGames;

namespace VideoGamesRental.Application.VideoGames.SaveVideoGame;

public record SaveVideoGameCommand(VideoGame VideoGame) : IRequest;