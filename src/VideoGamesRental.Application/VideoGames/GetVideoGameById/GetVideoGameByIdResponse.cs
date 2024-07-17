using LanguageExt;
using VideoGamesRental.Domain.VideoGames;

namespace VideoGamesRental.Application.VideoGames.GetVideoGameById;

public record GetVideoGameByIdResponse(Option<VideoGame> VideoGame);