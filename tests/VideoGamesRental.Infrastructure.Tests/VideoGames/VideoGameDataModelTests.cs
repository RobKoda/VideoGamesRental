using FluentAssertions;
using VideoGamesRental.Infrastructure.VideoGames;

namespace VideoGamesRental.Infrastructure.Tests.VideoGames;

public class VideoGameDataModelTests
{
    [Fact]
    public void NewVideoGameDataModel_ShouldInitializeEmptyStrings()
    {
        // Act
        var videoGame = new VideoGameDataModel();
        
        // Assert
        videoGame.Name.Should().BeEmpty();
    }
}