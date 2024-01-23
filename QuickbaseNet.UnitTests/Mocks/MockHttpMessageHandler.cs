using System.Net;

namespace QuickbaseNet.UnitTests.Mocks;

public class MockHttpMessageHandler : HttpMessageHandler
{
    public string ResponseContent { get; set; }
    public HttpStatusCode ResponseStatusCode { get; set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new HttpResponseMessage
        {
            StatusCode = ResponseStatusCode,
            Content = new StringContent(ResponseContent)
        });
    }
}