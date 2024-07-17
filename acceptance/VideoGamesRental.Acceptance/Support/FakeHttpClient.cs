using System.Text;
using System.Text.Json;
using VideoGamesRental.Acceptance.Contexts;

// ReSharper disable UnusedMember.Global - Methods might be used in the future

namespace VideoGamesRental.Acceptance.Support;

// ReSharper disable once ClassNeverInstantiated.Global - Instantiated by SpecFlow
public class FakeHttpClient
{
    private readonly AcceptanceContext _context;

    protected FakeHttpClient(AcceptanceContext inContext)
    {
        _context = inContext;
    }

    public HttpResponseMessage? HttpResponse { get; private set; }

    private HttpClient Client => _context.HttpClient;

    public async Task<string> ReadResponseContentAsync()
    {
        if (HttpResponse is null)
        {
            throw new ArgumentException($"{HttpResponse} cannot be null.");
        }

        return await HttpResponse.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
    }

    public async Task ProcessRequest(HttpMethod inMethod, string inRelativeUri) =>
        HttpResponse =
            HttpResponse = await ProcessRequest(await CreateRequestAsync(inMethod, inRelativeUri));

    public async Task ProcessRequest<TRequest>(HttpMethod inMethod, string inRelativeUri, TRequest inData) =>
        HttpResponse = await ProcessRequest(await CreateRequestAsync(inMethod, inRelativeUri, inData));

    private async Task<HttpRequestMessage> CreateRequestAsync<T>(HttpMethod inMethod, string inRelativeUri, T inData)
    {
        var requestMessage = await CreateRequestAsync(inMethod, inRelativeUri);
        requestMessage.Content =
            new StringContent(JsonSerializer.Serialize(inData), Encoding.UTF8, "application/json");
        return requestMessage;
    }

    private Task<HttpRequestMessage> CreateRequestAsync(HttpMethod inMethod, string inRelativeUri) => Task.FromResult(
        new HttpRequestMessage
        {
            Method = inMethod,
            RequestUri =
                new Uri(
                    Client.BaseAddress ??
                    throw new InvalidOperationException("HttpClient base address cannot be null."), inRelativeUri)
        });

    private async Task<HttpResponseMessage> ProcessRequest(HttpRequestMessage inMessage) =>
        await Client.SendAsync(inMessage);
}