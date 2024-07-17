using System.Net;
using VideoGamesRental.Acceptance.Support;

// ReSharper disable UnusedMember.Global - Might be used in the future

namespace VideoGamesRental.Acceptance.Drivers;

// ReSharper disable once UnusedType.Global - Might be used in the future
public abstract class TestDriver
{
    // ReSharper disable once MemberCanBePrivate.Global - Might be used in the future
    protected readonly FakeHttpClient Client;

    protected TestDriver(FakeHttpClient inClient)
    {
        Client = inClient;
    }
    
    public HttpStatusCode GetLastStatusCode() => 
        Client.HttpResponse!.StatusCode;
    
    public HttpContent GetLastResponseContent() => 
        Client.HttpResponse!.Content;
}