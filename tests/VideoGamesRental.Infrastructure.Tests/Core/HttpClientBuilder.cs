using Moq;
using Moq.Contrib.HttpClient;

namespace VideoGamesRental.Infrastructure.Tests.Core;

public static class HttpClientBuilder
{
    public static HttpClient CreateTestClient(this Mock<HttpMessageHandler> inHandler)
    {
        var httpClient = inHandler.CreateClient();
        httpClient.BaseAddress = new Uri("https://test.com");
        return httpClient;
    }
}