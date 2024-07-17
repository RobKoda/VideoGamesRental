using AutoFixture;
using FluentAssertions;
using LanguageExt;
using Moq;
using VideoGamesRental.Application.VideoGames.GetVideoGameById;
using VideoGamesRental.Application.VideoGames.GetVideoGames;
using VideoGamesRental.Application.VideoGames.Persistence;
using VideoGamesRental.Domain.VideoGames;
using VideoGamesRental.Tests.Core;

namespace VideoGamesRental.Application.Tests.VideoGames.GetVideoGameById;

public class GetVideoGameByIdHandlerTests
{
    private readonly IFixture _fixture;
    private readonly GetVideoGameByIdHandler _handler;
    private readonly Mock<IVideoGamesRepository> _mockRepository;

    public GetVideoGameByIdHandlerTests()
    {
        _fixture = new Fixture().Customize(new DateAndTimeOnlyCustomization());
        _mockRepository = new Mock<IVideoGamesRepository>();
        _handler = new GetVideoGameByIdHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handler_ShouldGetVideoGame_GivenIdMatch()
    {
        // Arrange
        var query = _fixture.Create<GetVideoGameByIdQuery>();
        var videoGame = _fixture.Create<VideoGame>();
        _mockRepository.Setup(inRepository => inRepository.GetVideoGameByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(videoGame);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.VideoGame.Case.Should().BeEquivalentTo(videoGame);
    }
    
    [Fact]
    public async Task Handler_ShouldReturnNone_GivenIdNoMatch()
    {
        // Arrange
        var query = _fixture.Create<GetVideoGameByIdQuery>();
        _mockRepository.Setup(inRepository => inRepository.GetVideoGameByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Option<VideoGame>.None);
        
        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.VideoGame.IsNone.Should().BeTrue();
    }
}