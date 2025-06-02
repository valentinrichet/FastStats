using FastStats.Domain.Strategies;

namespace FastStats.Domain.Tests.Strategies;

public sealed class NaiveComputationStrategyTests
{
    private readonly NaiveComputationStrategy _strategy = new();

    [Fact]
    public async Task ComputeMedian_OddNumberOfElements_ReturnsMiddleElement()
    {
        // Arrange
        IReadOnlyList<decimal> data = [5, 1, 9, 3, 7]; // Sorted: 1, 3, 5, 7, 9

        // Act
        var median = await _strategy.ComputeMedianAsync(data, CancellationToken.None);

        // Assert
        Assert.Equal(5m, median);
    }

    [Fact]
    public async Task ComputeMedianAsync_EvenNumberOfElements_ReturnsAverageOfMiddleTwo()
    {
        // Arrange
        IReadOnlyList<decimal> data = [6, 2, 8, 4]; // Sorted: 2, 4, 6, 8

        // Act
        var median = await _strategy.ComputeMedianAsync(data, CancellationToken.None);

        // Assert
        Assert.Equal(5m, median); // (4 + 6) / 2
    }

    [Fact]
    public async Task ComputeMedianAsync_SingleElement_ReturnsElementItself()
    {
        // Arrange
        IReadOnlyList<decimal> data = [42];

        // Act
        var median = await _strategy.ComputeMedianAsync(data, CancellationToken.None);

        // Assert
        Assert.Equal(42m, median);
    }

    [Fact]
    public async Task ComputeMedianAsync_EmptyData_ThrowsArgumentException()
    {
        // Act & Assert
        var exception =
            await Assert.ThrowsAsync<ArgumentException>(() => _strategy.ComputeMedianAsync([], CancellationToken.None));
        Assert.Equal("Data cannot be empty. (Parameter 'data')", exception.Message);
    }

    [Fact]
    public async Task ComputeAverageAsync_ValidData_ReturnsCorrectAverage()
    {
        // Arrange
        IReadOnlyList<decimal> data = [10, 20, 30, 40, 50]; // Sum = 150, Count = 5

        // Act
        var average = await _strategy.ComputeAverageAsync(data, CancellationToken.None);

        // Assert
        Assert.Equal(30m, average); // 150 / 5
    }

    [Fact]
    public async Task ComputeAverageAsync_EmptyData_ThrowsArgumentException()
    {
        // Act & Assert
        var exception =
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _strategy.ComputeAverageAsync([], CancellationToken.None));
        Assert.Equal("Data cannot be empty. (Parameter 'data')", exception.Message);
    }

    [Fact]
    public async Task ComputeVarianceAsync_ValidData_ReturnsCorrectVariance()
    {
        // Arrange
        IReadOnlyList<decimal> data = [1, 2, 3, 4, 5]; // Avg = 3
        // Differences: -2, -1, 0, 1, 2
        // Squared Diff: 4, 1, 0, 1, 4 => Sum = 10
        // Variance = 10 / 5 = 2

        // Act
        var variance = await _strategy.ComputeVarianceAsync(data, CancellationToken.None);

        // Assert
        Assert.Equal(2m, variance);
    }

    [Fact]
    public async Task ComputeVarianceAsync_AllElementsSame_ReturnsZero()
    {
        // Arrange
        var data = new List<decimal> { 7, 7, 7, 7, 7 };

        // Act
        var variance = await _strategy.ComputeVarianceAsync(data, CancellationToken.None);

        // Assert
        Assert.Equal(0m, variance);
    }


    [Fact]
    public async Task ComputeVarianceAsync_EmptyData_ThrowsArgumentException()
    {
        // Act & Assert
        var exception =
            await Assert.ThrowsAsync<ArgumentException>(() =>
                _strategy.ComputeVarianceAsync([], CancellationToken.None));
        Assert.Equal("Data cannot be empty. (Parameter 'data')", exception.Message);
    }
}