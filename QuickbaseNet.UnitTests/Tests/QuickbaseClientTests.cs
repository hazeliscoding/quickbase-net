using System.Net;
using System.Text.Json;
using Bogus;
using QuickbaseNet.Requests;
using QuickbaseNet.Responses;
using QuickbaseNet.Services;
using QuickbaseNet.UnitTests.Mocks;
using QuickbaseNet.UnitTests.Utility;

namespace QuickbaseNet.UnitTests.Tests;

public class QuickbaseClientTests
{
    private readonly MockHttpMessageHandler _mockHandler;
    private readonly QuickbaseClient _client;
    private const string TestRealm = "testRealm";
    private const string TestToken = "testToken";
    private const string BaseUrl = "http://localhost/";

    public QuickbaseClientTests()
    {
        _mockHandler = SetupMockHandlerWithSuccessResponse();
        _client = CreateConfiguredQuickbaseClient();
    }

    [Fact]
    public async Task Constructor_ThrowsArgumentNullException_WhenRealmIsNull()
    {
        // Arrange
        var realm = string.Empty;

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(new QuickbaseClient(realm, TestToken)));

        // Assert
        Assert.Equal("realm", exception.ParamName);
    }

    [Fact]
    public async Task Constructor_ThrowsArgumentNullException_WhenTokenIsNull()
    {
        // Arrange
        var token = string.Empty;

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => Task.FromResult(new QuickbaseClient(TestRealm, token)));

        // Assert
        Assert.Equal("userToken", exception.ParamName);
    }

    [Fact]
    public async Task QueryRecords_ReturnsSuccessResponse_WhenCalled()
    {
        // Arrange
        var request = new Builder().Build<QuickbaseQueryRequest>();

        // Act
        var response = await _client.QueryRecords(request);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Value);
        Assert.NotEmpty(response.Value.Data);
        Assert.NotEmpty(response.Value.Fields);
        Assert.NotNull(response.Value.Metadata);
    }

    [Fact]
    public async Task QueryRecords_ReturnsErrorResponse_When4xxOccurs()
    {
        // Arrange
        SetupMockHandlerWithErrorResponse();
        var request = new QuickbaseQueryRequest
        {
            From = "tableId",
            Where = "{1.CT.'query'}",
            Select = [1, 2, 3]
        };

        // Act
        var actualResponse = await _client.QueryRecords(request);

        // Assert
        Assert.False(actualResponse.IsSuccess);
        Assert.True(actualResponse.IsFailure);
        Assert.NotNull(actualResponse.QuickbaseError);
    }

    [Fact]
    public async Task QueryRecords_ReturnsErrorResponse_When5xxOccurs()
    {
        // Arrange
        _mockHandler.ResponseStatusCode = HttpStatusCode.InternalServerError;
        var request = new QuickbaseQueryRequest
        {
            From = "tableId",
            Where = "{1.CT.'query'}",
            Select = [1, 2, 3]
        };

        // Act
        var actualResponse = await _client.QueryRecords(request);

        // Assert
        Assert.False(actualResponse.IsSuccess);
        Assert.NotNull(actualResponse.QuickbaseError);
    }

    [Fact]
    public async Task InsertRecords_ReturnsSuccessResponse_WhenCalled()
    {
        // Arrange
        var request = new Builder().Build<InsertOrUpdateRecordRequest>();

        // Act
        var response = await _client.InsertRecords(request);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Response);
        Assert.Null(response.Error);
    }

    [Fact]
    public async Task InsertRecords_ReturnsErrorResponse_WhenFails()
    {
        // Arrange
        SetupMockHandlerWithErrorResponse();
        var request = new Builder().Build<InsertOrUpdateRecordRequest>();

        // Act
        var actualResponse = await _client.InsertRecords(request);

        // Assert
        Assert.False(actualResponse.IsSuccess);
        Assert.Null(actualResponse.Response);
        Assert.NotNull(actualResponse.Error);
    }

    [Fact]
    public async Task UpdateRecords_ReturnsSuccessResponse_WhenCalled()
    {
        // Arrange
        var request = new Builder().Build<InsertOrUpdateRecordRequest>();

        // Act
        var response = await _client.UpdateRecords(request);

        // Assert
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.Response);
        Assert.Null(response.Error);
    }

    [Fact]
    public async Task UpdateRecords_ReturnsErrorResponse_WhenFails()
    {
        // Arrange
        SetupMockHandlerWithErrorResponse();
        var request = new Builder().Build<InsertOrUpdateRecordRequest>();

        // Act
        var actualResponse = await _client.UpdateRecords(request);

        // Assert
        Assert.False(actualResponse.IsSuccess);
        Assert.Null(actualResponse.Response);
        Assert.NotNull(actualResponse.Error);
    }

    #region Helpers
    private void SetupMockHandlerWithErrorResponse()
    {
        var faker = new Faker();
        var errorResponse = new QuickbaseErrorResponse
        {
            Message = faker.Random.Words(),
            Description = faker.Lorem.Paragraph()
        };

        _mockHandler.ResponseContent = JsonSerializer.Serialize(errorResponse);
        _mockHandler.ResponseStatusCode = HttpStatusCode.BadRequest;
    }

    private MockHttpMessageHandler SetupMockHandlerWithSuccessResponse()
    {
        var mockJsonResponse = GetMockJsonResponse();
        return new MockHttpMessageHandler
        {
            ResponseContent = mockJsonResponse,
            ResponseStatusCode = HttpStatusCode.OK
        };
    }

    private QuickbaseClient CreateConfiguredQuickbaseClient()
    {
        var httpClient = new HttpClient(_mockHandler)
        {
            BaseAddress = new Uri(BaseUrl)
        };
        return new QuickbaseClient(TestRealm, TestToken)
        {
            Client = httpClient
        };
    }

    private static string GetMockJsonResponse()
    {
        // JSON response string...
        return @"{
                ""data"": [
                    {
                        ""6"": { ""value"": ""Andre Harris"" },
                        ""7"": { ""value"": 10 },
                        ""8"": { ""value"": ""2019-12-18T08:00:00Z"" }
                    }
                ],
                ""fields"": [
                    { ""id"": 6, ""label"": ""Full Name"", ""type"": ""text"" },
                    { ""id"": 7, ""label"": ""Amount"", ""type"": ""numeric"" },
                    { ""id"": 8, ""label"": ""Date time"", ""type"": ""date time"" }
                ],
                ""metadata"": {
                    ""totalRecords"": 10,
                    ""numRecords"": 1,
                    ""numFields"": 3,
                    ""skip"": 0
                }
            }";
    }

    #endregion
}