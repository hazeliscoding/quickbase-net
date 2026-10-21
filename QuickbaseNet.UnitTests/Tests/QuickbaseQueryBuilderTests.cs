using Newtonsoft.Json;
using QuickbaseNet.Helpers;

namespace QuickbaseNet.UnitTests.Tests;

public class QuickbaseQueryBuilderTests
{
    [Fact]
    public void Build_SetsSkipAndTop_WhenBothAreCalled()
    {
        // Act
        var request = new QuickbaseQueryBuilder().From("bck7").Skip(100).Top(50).Build();

        // Assert
        Assert.NotNull(request.Options);
        Assert.Equal(100, request.Options.Skip);
        Assert.Equal(50, request.Options.Top);
    }

    [Fact]
    public void Build_LeavesOptionsNull_WhenNeitherIsCalled()
    {
        // Act
        var request = new QuickbaseQueryBuilder().From("bck7").Select(3, 6).Build();

        // Assert
        Assert.Null(request.Options);
    }

    [Fact]
    public void Serialize_SendsSkipWithoutTop_WhenOnlySkipIsCalled()
    {
        // Arrange
        var request = new QuickbaseQueryBuilder().From("bck7").Skip(100).Build();

        // Act
        var json = JsonConvert.SerializeObject(request);

        // Assert
        Assert.Contains("\"skip\":100", json);
        Assert.DoesNotContain("\"top\"", json);
    }
}
