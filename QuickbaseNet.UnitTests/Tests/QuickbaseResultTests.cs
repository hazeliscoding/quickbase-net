using QuickbaseNet.Errors;

namespace QuickbaseNet.UnitTests.Tests;

public class QuickbaseResultTests
{
    [Fact]
    public void Constructor_ThrowsArgumentException_WhenInvalidErrorForSuccess()
    {
        // Arrange
        var isSuccess = true;
        var invalidError = QuickbaseError.ClientError("InvalidError", "Invalid error occurred", "Description");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new TestableQuickbaseResult(isSuccess, invalidError));
    }

    [Fact]
    public void Constructor_ThrowsArgumentException_WhenInvalidErrorForFailure()
    {
        // Arrange
        var isSuccess = false;
        var invalidError = QuickbaseError.None;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new TestableQuickbaseResult(isSuccess, invalidError));
    }
}

internal class TestableQuickbaseResult : QuickbaseResult
{
    public TestableQuickbaseResult(bool isSuccess, QuickbaseError quickbaseError)
        : base(isSuccess, quickbaseError)
    {
        // This constructor allows access to the protected internal constructor of QuickbaseResult
    }
}
