using System.Net;
using FluentAssertions;
using TechTalk.SpecFlow;
using VideoGamesRental.Acceptance.Drivers;

namespace VideoGamesRental.Acceptance.Steps;

[Binding]
public class HealthcheckSteps
{
    private readonly HealthcheckDriver _driver;

    public HealthcheckSteps(HealthcheckDriver inDriver)
    {
        _driver = inDriver;
    }
    
    [When("I GET the Healthcheck")]
    public async Task WhenIGetTheHealthcheck() =>
        await _driver.GetHealthcheckAsync();

    [Then("I should receive an OK result")]
    public void ThenIShouldReceiveAnOkResult() =>
        _driver.GetHealthcheckStatusCode().Should().Be(HttpStatusCode.OK);
}