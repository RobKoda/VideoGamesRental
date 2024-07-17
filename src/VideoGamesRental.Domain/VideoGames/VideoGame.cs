namespace VideoGamesRental.Domain.VideoGames;

public record VideoGame(
    Guid Id,
    string Name,
    DateOnly ReleaseDate
    );