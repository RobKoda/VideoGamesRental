using AutoFixture;
using FluentAssertions;
using Mapster;
using VideoGamesRental.Domain.VideoGames;
using VideoGamesRental.Infrastructure.Tests.Core;
using VideoGamesRental.Infrastructure.VideoGames;

namespace VideoGamesRental.Infrastructure.Tests.VideoGames;

[Collection("Sequential")]
public class VideoGamesRepositoryTests : IDisposable
{
    private readonly DataBuilder<ApplicationContext> _dataBuilder;
    private readonly IFixture _fixture;
    private readonly VideoGamesRepository _repository;
    
    public VideoGamesRepositoryTests()
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ServicesRegistration).Assembly);
        
        _dataBuilder = DataBuilder<ApplicationContext>.Build();
        _repository = new VideoGamesRepository(_dataBuilder.Context);
        _fixture = FixtureBuilder.GetFixture();
    }
    
    public void Dispose()
    {
        _dataBuilder.Context.Database.EnsureDeleted();
        GC.SuppressFinalize(this);
    }
    
    [Fact]
    public async Task GetVideoGamesAsync_ShouldReturnVideoGames()
    {
        // Arrange
        var videoGames = _fixture.CreateMany<VideoGameDataModel>();
        await _dataBuilder.WithEntities(videoGames).CommitAsync();
        
        // Act
        var result = await _repository.GetVideoGamesAsync();
        
        // Assert
        result.Should().BeEquivalentTo(videoGames.Adapt<IEnumerable<VideoGame>>());
    }
    
    [Fact]
    public async Task GetVideoGameByIdAsync_ShouldReturnVideoGame()
    {
        // Arrange
        var videoGames = _fixture.CreateMany<VideoGameDataModel>().ToList();
        await _dataBuilder.WithEntities(videoGames).CommitAsync();
        
        // Act
        var result = await _repository.GetVideoGameByIdAsync(videoGames.First().Id);
        
        // Assert
        result.Case.Should().BeEquivalentTo(videoGames.First().Adapt<VideoGame>());
    }
    
    [Fact]
    public async Task GetVideoGameByIdAsync_ShouldReturnNone_GivenIdNotFound()
    {
        // Arrange
        var videoGames = _fixture.CreateMany<VideoGameDataModel>().ToList();
        await _dataBuilder.WithEntities(videoGames).CommitAsync();
        
        // Act
        var result = await _repository.GetVideoGameByIdAsync(Guid.NewGuid());
        
        // Assert
        result.IsNone.Should().BeTrue();
    }
}