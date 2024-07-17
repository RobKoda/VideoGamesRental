using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VideoGamesRental.Api.Controllers;
using VideoGamesRental.Application.VideoGames.DeleteVideoGame;
using VideoGamesRental.Application.VideoGames.GetVideoGameById;
using VideoGamesRental.Application.VideoGames.GetVideoGames;
using VideoGamesRental.Application.VideoGames.SaveVideoGame;
using VideoGamesRental.Domain.VideoGames;
using VideoGamesRental.Tests.Core;

namespace VideoGamesRental.Api.Tests.Controllers;

public class VideoGamesControllerTests
{
    private readonly IFixture _fixture;
    private readonly VideoGamesController _controller;
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<IValidator<SaveVideoGameRequest>> _mockSaveVideoGameRequestValidator;
    private readonly Mock<IValidator<DeleteVideoGameRequest>> _mockDeleteVideoGameRequestValidator;
    
    public VideoGamesControllerTests()
    {
        _fixture = new Fixture().Customize(new DateAndTimeOnlyCustomization());
        _mockMediator = new Mock<IMediator>();
        _mockSaveVideoGameRequestValidator = new Mock<IValidator<SaveVideoGameRequest>>();
        _mockDeleteVideoGameRequestValidator = new Mock<IValidator<DeleteVideoGameRequest>>();
        _controller = new VideoGamesController(_mockMediator.Object, _mockSaveVideoGameRequestValidator.Object, _mockDeleteVideoGameRequestValidator.Object);
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
    
    [Fact]
    public async Task SaveVideoGameAsync_ShouldReturnOkAndSaveVideoGame()
    {
        // Arrange
        var request = _fixture.Create<VideoGame>();
        _mockSaveVideoGameRequestValidator
            .Setup(inValidator => inValidator.ValidateAsync(
                It.IsAny<SaveVideoGameRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        // Act
        var result = await _controller.SaveVideoGameAsync(request);

        // Assert
        _mockMediator.Verify(inMediator => inMediator.Send(It.IsAny<SaveVideoGameCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task SaveVideoGameAsync_ShouldReturnBadRequest_GivenValidationFailed()
    {
        // Arrange
        var request = _fixture.Create<VideoGame>();
        _mockSaveVideoGameRequestValidator
            .Setup(inValidator => inValidator.ValidateAsync(
                It.IsAny<SaveVideoGameRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult { Errors = [..new[] { new ValidationFailure() }] });

        // Act
        var result = await _controller.SaveVideoGameAsync(request);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
    
    [Fact]
    public async Task DeleteVideoGameAsync_ShouldReturnOkAndDeleteVideoGame()
    {
        // Arrange
        var request = _fixture.Create<DeleteVideoGameRequest>();
        _mockDeleteVideoGameRequestValidator
            .Setup(inValidator => inValidator.ValidateAsync(
                It.IsAny<DeleteVideoGameRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        // Act
        var result = await _controller.DeleteVideoGameAsync(request.Id);

        // Assert
        _mockMediator.Verify(inMediator => inMediator.Send(It.IsAny<DeleteVideoGameCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType<OkResult>();
    }

    [Fact]
    public async Task DeleteVideoGameAsync_ShouldReturnBadRequest_GivenValidationFailed()
    {
        // Arrange
        var request = _fixture.Create<DeleteVideoGameRequest>();
        _mockDeleteVideoGameRequestValidator
            .Setup(inValidator => inValidator.ValidateAsync(
                It.IsAny<DeleteVideoGameRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult { Errors = [..new[] { new ValidationFailure() }] });

        // Act
        var result = await _controller.DeleteVideoGameAsync(request.Id);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }
}