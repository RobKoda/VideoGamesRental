using AutoFixture;
using FluentAssertions;
using Moq;
using VideoGamesRental.Application.VideoGames.GetVideoGames;
using VideoGamesRental.Application.VideoGames.Persistence;
using VideoGamesRental.Domain.VideoGames;
using VideoGamesRental.Tests.Core;

namespace VideoGamesRental.Application.Tests.VideoGames.GetVideoGames;

public class GetVideoGamesHandlerTests
{
    private readonly IFixture _fixture;
    private readonly GetVideoGamesHandler _handler;
    private readonly Mock<IVideoGamesRepository> _mockRepository;

    public GetVideoGamesHandlerTests()
    {
        _fixture = new Fixture().Customize(new DateAndTimeOnlyCustomization());
        _mockRepository = new Mock<IVideoGamesRepository>();
        _handler = new GetVideoGamesHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handler_ShouldGetVideoGames()
    {
        // Arrange
        var query = _fixture.Create<GetVideoGamesQuery>();
        var videoGames = _fixture.Create<IEnumerable<VideoGame>>().ToList();
        _mockRepository.Setup(inRepository => inRepository.GetVideoGamesAsync())
            .ReturnsAsync(videoGames);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.VideoGames.Should().BeEquivalentTo(videoGames);
    }
}