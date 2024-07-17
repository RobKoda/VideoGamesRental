using LanguageExt;
using VideoGamesRental.Domain.VideoGames;

namespace VideoGamesRental.Application.VideoGames.Persistence;

public interface IVideoGamesRepository
{
    Task<IEnumerable<VideoGame>> GetVideoGamesAsync();
    Task<Option<VideoGame>> GetVideoGameByIdAsync(Guid inId);
}