using AutoFixture;
using FluentAssertions;
using FluentValidation;
using Moq;
using VideoGamesRental.Application.Tests.Core;
using VideoGamesRental.Application.VideoGames.DeleteVideoGame;
using VideoGamesRental.Application.VideoGames.Persistence;

namespace VideoGamesRental.Application.Tests.VideoGames.DeleteVideoGame;

public class DeleteVideoGameRequestValidationTests
{
    private readonly DeleteVideoGameRequestValidation _validation;
    private readonly IFixture _fixture;
    private readonly Mock<IVideoGamesRepository> _mockRepository;

    public DeleteVideoGameRequestValidationTests()
    {
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        _mockRepository = new Mock<IVideoGamesRepository>();
        _mockRepository.Setup(inRepository => inRepository.AnyVideoGameAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _mockRepository.Setup(inRepository => inRepository.CanVideoGameBeDeletedAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        
        _fixture = new Fixture();
        _validation = new DeleteVideoGameRequestValidation(_mockRepository.Object);
    }

    [Fact]
    public async Task ValidateAsync_ShouldSucceed()
    {
        // Arrange
        var request = _fixture.Create<DeleteVideoGameRequest>();

        // Act
        var result = await _validation.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task ValidateAsync_ShouldFailWithErrorMessage_GivenGameNotFound()
    {
        // Arrange
        var request = _fixture.Create<DeleteVideoGameRequest>();
        _mockRepository.Setup(inRepository => inRepository.AnyVideoGameAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act
        var result = await _validation.ValidateAsync(request);

        // Assert
        result.ShouldError($"{nameof(DeleteVideoGameRequest.Id)}",
            DeleteVideoGameRequestValidationErrors.VideoGameNotFound);
    }
    
    [Fact]
    public async Task ValidateAsync_ShouldFailWithErrorMessage_GivenGameCannotBeDeleted()
    {
        // Arrange
        var request = _fixture.Create<DeleteVideoGameRequest>();
        _mockRepository.Setup(inRepository => inRepository.CanVideoGameBeDeletedAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);
        
        // Act
        var result = await _validation.ValidateAsync(request);
        
        // Assert
        result.ShouldError($"{nameof(DeleteVideoGameRequest.Id)}",
            DeleteVideoGameRequestValidationErrors.VideoGameCannotBeDeleted);
    }
}