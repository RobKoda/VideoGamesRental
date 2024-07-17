using LanguageExt;
using VideoGamesRental.Application.VideoGames.Persistence;
using VideoGamesRental.Domain.VideoGames;
using VideoGamesRental.Infrastructure.Core;

namespace VideoGamesRental.Infrastructure.VideoGames;

public class VideoGamesRepository(ApplicationContext inContext) : IVideoGamesRepository
{
    public async Task<IEnumerable<VideoGame>> GetVideoGamesAsync() => 
        await inContext.GetAllAsync<VideoGameDataModel, VideoGame>();
    
    public async Task<Option<VideoGame>> GetVideoGameByIdAsync(Guid inId) => 
        await inContext.GetSingleAsync<VideoGameDataModel, VideoGame>(inId);
}