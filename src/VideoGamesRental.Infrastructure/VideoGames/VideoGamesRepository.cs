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
    
    public async Task SaveVideoGameAsync(VideoGame inVideoGame) => 
        await inContext.SaveAsync<VideoGameDataModel, VideoGame>(inVideoGame);
    
    public async Task<bool> CanVideoGameBeDeletedAsync(Guid inId) =>
        await inContext.CanBeDeleted<VideoGameDataModel>(inId);
    
    public async Task<bool> AnyVideoGameAsync(Guid inId) =>
        await inContext.AnyAsync<VideoGameDataModel>(inId);
    
    public async Task DeleteVideoGameAsync(Guid inId) =>
        await inContext.DeleteAsync<VideoGameDataModel>(inId);
}