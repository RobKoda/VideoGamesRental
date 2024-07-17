using AutoFixture;
using Moq;
using VideoGamesRental.Application.VideoGames.Persistence;
using VideoGamesRental.Application.VideoGames.SaveVideoGame;
using VideoGamesRental.Domain.VideoGames;
using VideoGamesRental.Tests.Core;

namespace VideoGamesRental.Application.Tests.VideoGames.SaveVideoGame;

public class SaveVideoGameHandlerTests
{
    private readonly IFixture _fixture;
    private readonly SaveVideoGameHandler _handler;
    private readonly Mock<IVideoGamesRepository> _mockRepository;

    public SaveVideoGameHandlerTests()
    {
        _fixture = new Fixture().Customize(new DateAndTimeOnlyCustomization());
        _mockRepository = new Mock<IVideoGamesRepository>();
        _handler = new SaveVideoGameHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldSaveVideoGame()
    {
        // Arrange
        var command = _fixture.Create<SaveVideoGameCommand>();

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(inRepository => inRepository.SaveVideoGameAsync(It.IsAny<VideoGame>()), Times.Once);
    }
}