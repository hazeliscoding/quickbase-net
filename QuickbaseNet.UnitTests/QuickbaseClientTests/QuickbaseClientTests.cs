using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Bogus;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using QuickbaseNet.Models;
using QuickbaseNet.Requests;
using QuickbaseNet.Responses;
using QuickbaseNet.Services;
using QuickbaseNet.UnitTests.Mocks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace QuickbaseNet.UnitTests.QuickbaseClientTests;

public class QuickbaseClientTests
{
    private readonly MockHttpMessageHandler _mockHandler;
    private readonly QuickbaseClient _client;

    public QuickbaseClientTests()
    {
        var mockJsonResponse = @"{
          ""data"": [
            {
              ""6"": {
                ""value"": ""Andre Harris""
              },
              ""7"": {
                ""value"": 10
              },
              ""8"": {
                ""value"": ""2019-12-18T08:00:00Z""
              }
            }
          ],
          ""fields"": [
            {
              ""id"": 6,
              ""label"": ""Full Name"",
              ""type"": ""text""
            },
            {
              ""id"": 7,
              ""label"": ""Amount"",
              ""type"": ""numeric""
            },
            {
              ""id"": 8,
              ""label"": ""Date time"",
              ""type"": ""date time""
            }
          ],
          ""metadata"": {
            ""totalRecords"": 10,
            ""numRecords"": 1,
            ""numFields"": 3,
            ""skip"": 0
          }
        }";

        _mockHandler = new MockHttpMessageHandler
        {
            ResponseContent = mockJsonResponse, // Updated mock response content
            ResponseStatusCode = HttpStatusCode.OK
        };

        var httpClient = new HttpClient(_mockHandler)
        {
            BaseAddress = new Uri("http://localhost/") // Set a dummy BaseAddress
        };
        _client = new QuickbaseClient("testRealm", "testToken")
        {
            Client = httpClient // Injecting our mock HttpClient
        };
    }

    [Fact]
    public async Task QueryRecords_Success()
    {
        // Arrange
        var request = new QuickbaseQueryRequest();

        // Act
        var (response, error, isSuccess) = await _client.QueryRecords(request);

        // Assert
        Assert.True(isSuccess);
        Assert.NotNull(response);
        Assert.Null(error);


        // Additional assertions to verify the structure and content of the response
        Assert.NotEmpty(response.Data);
        Assert.NotEmpty(response.Fields);
        Assert.NotNull(response.Metadata);
    }

    [Fact]
    public async Task QueryRecords_Failure()
    {
        // Arrange - Set up mock handler to return error response
        var faker = new Faker();
        var errorResponse = new QuickbaseErrorResponse
        {
            Message = faker.Random.Words(),
            Description = faker.Lorem.Paragraph()
        };

        _mockHandler.ResponseContent = JsonSerializer.Serialize(errorResponse);
        _mockHandler.ResponseStatusCode = HttpStatusCode.BadRequest;

        var request = new QuickbaseQueryRequest
        {
            From = "tableId",
            Where = "{1.CT.'query'}",
            Select = new List<int> { 1, 2, 3 }
        };

        // Act
        var (actualResponse, actualError, actualSuccessResult) = await _client.QueryRecords(request);

        // Assert
        Assert.False(actualSuccessResult);
        Assert.Null(actualResponse);
        Assert.NotNull(actualError);
        Assert.Equal(errorResponse.Message, actualError.Message);
        Assert.Equal(errorResponse.Description, actualError.Description);
    }
}