using MediatR;

namespace VideoGamesRental.Application.VideoGames.DeleteVideoGame;

public record DeleteVideoGameCommand(Guid Id) : IRequest;