using FastStats.Domain.ValueObjects;

namespace FastStats.Domain.Tests.ValueObjects;

public sealed class StatisticalSummaryTests
{
    [Fact]
    public void Constructor_AssignsPropertiesCorrectly()
    {
        // Arrange
        var median = 10.5m;
        var variance = 5.2m;
        var average = 12.8m;

        // Act
        var summary = new StatisticalSummary(median, variance, average);

        // Assert
        Assert.Equal(median, summary.Median);
        Assert.Equal(variance, summary.Variance);
        Assert.Equal(average, summary.Average);
    }

    [Fact]
    public void Equality_SameValues_ReturnsTrue()
    {
        // Arrange
        var median = 10.5m;
        var variance = 5.2m;
        var average = 12.8m;
        var summary1 = new StatisticalSummary(median, variance, average);
        var summary2 = new StatisticalSummary(median, variance, average);

        // Act & Assert
        Assert.True(summary1.Equals(summary2));
        Assert.True(summary1 == summary2);
        Assert.False(summary1 != summary2);
        Assert.Equal(summary1.GetHashCode(), summary2.GetHashCode());
    }

    [Fact]
    public void Equality_DifferentMedian_ReturnsFalse()
    {
        // Arrange
        var variance = 5.2m;
        var average = 12.8m;
        var summary1 = new StatisticalSummary(10.5m, variance, average);
        var summary2 = new StatisticalSummary(11.5m, variance, average);

        // Act & Assert
        Assert.False(summary1.Equals(summary2));
        Assert.False(summary1 == summary2);
        Assert.True(summary1 != summary2);
        Assert.NotEqual(summary1.GetHashCode(), summary2.GetHashCode());
    }

    [Fact]
    public void Equality_DifferentVariance_ReturnsFalse()
    {
        // Arrange
        var median = 10.5m;
        var average = 12.8m;
        var summary1 = new StatisticalSummary(median, 5.2m, average);
        var summary2 = new StatisticalSummary(median, 6.2m, average);

        // Act & Assert
        Assert.False(summary1.Equals(summary2));
        Assert.False(summary1 == summary2);
        Assert.True(summary1 != summary2);
        Assert.NotEqual(summary1.GetHashCode(), summary2.GetHashCode());
    }

    [Fact]
    public void Equality_DifferentAverage_ReturnsFalse()
    {
        // Arrange
        var median = 10.5m;
        var variance = 5.2m;
        var summary1 = new StatisticalSummary(median, variance, 12.8m);
        var summary2 = new StatisticalSummary(median, variance, 13.8m);

        // Act & Assert
        Assert.False(summary1.Equals(summary2));
        Assert.False(summary1 == summary2);
        Assert.True(summary1 != summary2);
        Assert.NotEqual(summary1.GetHashCode(), summary2.GetHashCode());
    }

    [Fact]
    public void Equality_ComparingWithNull_ReturnsFalse()
    {
        // Arrange
        var summary = new StatisticalSummary(10m, 5m, 12m);

        // Act & Assert
        Assert.False(summary.Equals(null));
    }

    [Fact]
    public void Deconstruct_AssignsPropertiesToVariables()
    {
        // Arrange
        var expectedMedian = 10.5m;
        var expectedVariance = 5.2m;
        var expectedAverage = 12.8m;
        var summary = new StatisticalSummary(expectedMedian, expectedVariance, expectedAverage);

        // Act
        var (median, variance, average) = summary;

        // Assert
        Assert.Equal(expectedMedian, median);
        Assert.Equal(expectedVariance, variance);
        Assert.Equal(expectedAverage, average);
    }
}