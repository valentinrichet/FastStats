namespace FastStats.Domain.Tests;

public sealed class DatasetTests
{
    [Fact]
    public void Constructor_WithValidData_InitializesCorrectly()
    {
        // Arrange
        IReadOnlyList<decimal> validData = [10, 20, 30];

        // Act
        var dataset = new Dataset(validData);

        // Assert
        Assert.NotEqual(Guid.Empty, dataset.Id);
        Assert.Equivalent(validData, dataset.Data);
    }

    [Fact]
    public void Constructor_EmptyData_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Dataset([]));
        Assert.Equal("Dataset cannot be empty. (Parameter 'data')", exception.Message);
    }
}