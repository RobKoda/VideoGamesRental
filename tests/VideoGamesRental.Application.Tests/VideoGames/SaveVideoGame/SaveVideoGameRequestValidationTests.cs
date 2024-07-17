using AutoFixture;
using FluentAssertions;
using FluentValidation;
using VideoGamesRental.Application.Tests.Core;
using VideoGamesRental.Application.VideoGames.SaveVideoGame;
using VideoGamesRental.Domain.VideoGames;
using VideoGamesRental.Tests.Core;

namespace VideoGamesRental.Application.Tests.VideoGames.SaveVideoGame;

public class SaveVideoGameRequestValidationTests
{
    private readonly SaveVideoGameRequestValidation _validation;
    private readonly IFixture _fixture;
    
    public SaveVideoGameRequestValidationTests()
    {
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        
        _fixture = new Fixture().Customize(new DateAndTimeOnlyCustomization());
        _validation = new SaveVideoGameRequestValidation();
    }
    
    private SaveVideoGameRequest GetValidSaveVideoGameRequest()
    {
        //new string('*', inMaxSize)
        var videoGame = _fixture.Build<VideoGame>()
            .With(inVideoGame => inVideoGame.Name, new string('*', 128))
            .Create();
        return new SaveVideoGameRequest(videoGame);
    }
    
    [Fact]
    public async Task ValidateAsync_ShouldSucceed()
    {
        // Arrange
        var request = GetValidSaveVideoGameRequest();
        
        // Act
        var result = await _validation.ValidateAsync(request);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public async Task ValidateAsync_ShouldFailWithErrorMessage_GivenLastNameTooLong() =>
        await _validation.ValidateMaxLength(
            GetValidSaveVideoGameRequest,
            (inRequest, inValue) => new SaveVideoGameRequest(inRequest.VideoGame with
            {
                Name = inValue
            }),
            128,
            $"{nameof(SaveVideoGameRequest.VideoGame)}.{nameof(SaveVideoGameRequest.VideoGame.Name)}",
            SaveVideoGameRequestValidationErrors.NameTooLong
        );
    
}