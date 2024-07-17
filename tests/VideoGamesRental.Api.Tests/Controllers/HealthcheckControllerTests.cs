using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VideoGamesRental.Api.Controllers;

namespace VideoGamesRental.Api.Tests.Controllers;

public class HealthcheckControllerTests
{
    private readonly HealthcheckController _controller = new();

    [Fact]
    public void Get_ShouldReturnOkResponse()
    {
        // Act
        var result = _controller.Get();
        
        // Assert
        result.Should().BeOfType<OkResult>();
    }
}