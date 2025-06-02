using FastStats.Domain.Strategies;
using FastStats.Domain.ValueObjects;
using NSubstitute;

namespace FastStats.Domain.Tests;

public sealed class StatisticalComputationTests
{
    private readonly Dataset _validDataset = new([1m]);

    [Fact]
    public void Constructor_InitializesPropertiesCorrectly()
    {
        // Act
        var statisticalComputation = new StatisticalComputation(_validDataset);

        // Assert
        Assert.NotEqual(Guid.Empty, statisticalComputation.Id);
        Assert.Equal(_validDataset, statisticalComputation.Dataset);
        Assert.Equal(ComputationStrategyIdentifier.None, statisticalComputation.ComputationStrategyIdentifier);
        Assert.Null(statisticalComputation.Results);
        Assert.Null(statisticalComputation.ComputedStartedAt);
        Assert.Null(statisticalComputation.ComputedEndedAt);
    }

    [Fact]
    public async Task ComputeAsync_UpdateAccordinglyStatisticalComputation()
    {
        // Arrange
        const string computationStrategyName = "MockStrategy";
        const decimal expectedMedian = 1m;
        const decimal expectedVariance = 2m;
        const decimal expectedAverage = 3m;
        var statisticalComputation = new StatisticalComputation(_validDataset);
        var computationStrategy = Substitute.For<IComputationStrategy>();
        computationStrategy.Identifier.Returns(new ComputationStrategyIdentifier(computationStrategyName));
        computationStrategy.ComputeMedianAsync(Arg.Any<IReadOnlyList<decimal>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(expectedMedian));
        computationStrategy.ComputeVarianceAsync(Arg.Any<IReadOnlyList<decimal>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(expectedVariance));
        computationStrategy.ComputeAverageAsync(Arg.Any<IReadOnlyList<decimal>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(expectedAverage));

        // Act
        var beforeComputationStartedAt = DateTime.UtcNow;
        await statisticalComputation.ComputeAsync(computationStrategy, CancellationToken.None);
        var afterComputationEndedAt = DateTime.UtcNow;

        // Assert
        await computationStrategy.Received(1)
            .ComputeMedianAsync(Arg.Any<IReadOnlyList<decimal>>(), Arg.Any<CancellationToken>());
        await computationStrategy.Received(1)
            .ComputeVarianceAsync(Arg.Any<IReadOnlyList<decimal>>(), Arg.Any<CancellationToken>());
        await computationStrategy.Received(1)
            .ComputeAverageAsync(Arg.Any<IReadOnlyList<decimal>>(), Arg.Any<CancellationToken>());
        Assert.NotEqual(Guid.Empty, statisticalComputation.Id);
        Assert.Equal(_validDataset, statisticalComputation.Dataset);
        Assert.Equal(computationStrategyName, statisticalComputation.ComputationStrategyIdentifier.Name);
        Assert.NotNull(statisticalComputation.Results);
        Assert.Equal(expectedMedian, statisticalComputation.Results.Median);
        Assert.Equal(expectedVariance, statisticalComputation.Results.Variance);
        Assert.Equal(expectedAverage, statisticalComputation.Results.Average);
        Assert.NotNull(statisticalComputation.ComputedStartedAt);
        Assert.NotNull(statisticalComputation.ComputedEndedAt);
        Assert.InRange(statisticalComputation.ComputedStartedAt.Value, beforeComputationStartedAt,
            statisticalComputation.ComputedEndedAt.Value);
        Assert.InRange(statisticalComputation.ComputedEndedAt.Value, statisticalComputation.ComputedStartedAt.Value,
            afterComputationEndedAt);
    }
}