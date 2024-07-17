using AutoFixture;
using Moq;
using VideoGamesRental.Application.VideoGames.DeleteVideoGame;
using VideoGamesRental.Application.VideoGames.Persistence;

namespace VideoGamesRental.Application.Tests.VideoGames.DeleteVideoGame;

public class DeleteVideoGameHandlerTests
{
    private readonly IFixture _fixture;
    private readonly DeleteVideoGameHandler _handler;
    private readonly Mock<IVideoGamesRepository> _mockRepository;

    public DeleteVideoGameHandlerTests()
    {
        _fixture = new Fixture();
        _mockRepository = new Mock<IVideoGamesRepository>();
        _handler = new DeleteVideoGameHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteVideoGame()
    {
        // Arrange
        var command = _fixture.Create<DeleteVideoGameCommand>();

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(inRepository => inRepository.DeleteVideoGameAsync(It.IsAny<Guid>()), Times.Once);
    }
}