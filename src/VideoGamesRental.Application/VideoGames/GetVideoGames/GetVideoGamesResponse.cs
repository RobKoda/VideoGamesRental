using VideoGamesRental.Domain.VideoGames;

namespace VideoGamesRental.Application.VideoGames.GetVideoGames;

public record GetVideoGamesResponse(IEnumerable<VideoGame> VideoGames);