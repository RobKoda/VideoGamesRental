using AutoFixture;
using FluentAssertions;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VideoGamesRental.Api.Controllers;
using VideoGamesRental.Application.VideoGames.GetVideoGameById;
using VideoGamesRental.Application.VideoGames.GetVideoGames;
using VideoGamesRental.Domain.VideoGames;
using VideoGamesRental.Tests.Core;

namespace VideoGamesRental.Api.Tests.Controllers;

public class VideoGamesControllerTests
{
    private readonly IFixture _fixture;
    private readonly VideoGamesController _controller;
    private readonly Mock<IMediator> _mockMediator;
    
    public VideoGamesControllerTests()
    {
        _fixture = new Fixture().Customize(new DateAndTimeOnlyCustomization());
        _mockMediator = new Mock<IMediator>();
        _controller = new VideoGamesController(_mockMediator.Object);
    }
    
    [Fact]
    public async Task GetVideoGamesAsync_ShouldReturnVideoGames()
    {
        // Arrange
        var response = _fixture.Create<GetVideoGamesResponse>();
        _mockMediator
            .Setup(inMediator => inMediator.Send(It.IsAny<GetVideoGamesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        
        // Act
        var result = await _controller.GetVideoGamesAsync();
        
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = (result as OkObjectResult)!;
        okResult.Value.Should().Be(response.VideoGames);
    }
    
    [Fact]
    public async Task GetVideoGameByIdAsync_ShouldReturnNotFound_GivenNoVideoGameFound()
    {
        // Arrange
        _mockMediator
            .Setup(inMediator => inMediator.Send(It.IsAny<GetVideoGameByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetVideoGameByIdResponse(Option<VideoGame>.None));
        
        // Act
        var result = await _controller.GetVideoGameByIdAsync(Guid.NewGuid());
        
        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
    
    [Fact]
    public async Task GetVideoGameByIdAsync_ShouldReturnVideoGame()
    {
        // Arrange
        var response = _fixture.Create<GetVideoGameByIdResponse>();
        _mockMediator
            .Setup(inMediator => inMediator.Send(It.IsAny<GetVideoGameByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        
        // Act
        var result = await _controller.GetVideoGameByIdAsync(Guid.NewGuid());
        
        // Assert
        result.Should().BeOfType<OkObjectResult>();
        var okResult = (result as OkObjectResult)!;
        var option = response.VideoGame;
        okResult.Value.Should().Be(option.Case);
    }
}