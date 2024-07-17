using Microsoft.Extensions.DependencyInjection;
using VideoGamesRental.Acceptance.Support;
using VideoGamesRental.Api;

namespace VideoGamesRental.Acceptance.Contexts;

// ReSharper disable once ClassNeverInstantiated.Global - Auto instantiated by SpecFlow
public sealed class AcceptanceContext
{
    public IServiceProvider ServiceProvider { get; }
    public HttpClient HttpClient { get; }
    
    public AcceptanceContext()
    {
        var acceptanceWebApplicationFactory = new AcceptanceWebApplicationFactory<Program>();
        
        ServiceProvider = acceptanceWebApplicationFactory.Services.CreateScope().ServiceProvider;
        HttpClient = acceptanceWebApplicationFactory.CreateClient();
    }
}