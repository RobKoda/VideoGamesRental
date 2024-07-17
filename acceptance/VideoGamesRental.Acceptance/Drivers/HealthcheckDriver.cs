using System.Net;
using VideoGamesRental.Acceptance.Contexts;

namespace VideoGamesRental.Acceptance.Drivers;

// ReSharper disable once ClassNeverInstantiated.Global - Auto instantiated by SpecFlow
public class HealthcheckDriver
{
    private readonly AcceptanceContext _context;
    private HttpResponseMessage? _response;

    public HealthcheckDriver(AcceptanceContext inContext)
    {
        _context = inContext;    
    }
    
    public async Task GetHealthcheckAsync() =>
        _response = await _context.HttpClient.GetAsync("/api/Healthcheck");

    public HttpStatusCode GetHealthcheckStatusCode() => 
        _response!.StatusCode;
}