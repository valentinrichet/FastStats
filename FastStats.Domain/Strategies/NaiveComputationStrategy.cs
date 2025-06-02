using System.Collections.Immutable;
using FastStats.Domain.ValueObjects;

namespace FastStats.Domain.Strategies;

public sealed class NaiveComputationStrategy : IComputationStrategy
{
    public ComputationStrategyIdentifier Identifier => ComputationStrategyIdentifier.Naive;

    public Task<decimal> ComputeMedianAsync(IReadOnlyList<decimal> data, CancellationToken cancellationToken)
    {
        if (data.Count == 0) throw new ArgumentException("Data cannot be empty.", nameof(data));

        var sortedData = data
            .OrderBy(x => x)
            .ToImmutableArray();
        var median = data.Count % 2 == 1
            ? sortedData[data.Count / 2]
            : (sortedData[data.Count / 2 - 1] + sortedData[data.Count / 2]) / 2m;
        return Task.FromResult(median);
    }

    public async Task<decimal> ComputeVarianceAsync(IReadOnlyList<decimal> data, CancellationToken cancellationToken)
    {
        if (data.Count == 0) throw new ArgumentException("Data cannot be empty.", nameof(data));

        var mean = await ComputeAverageAsync(data, cancellationToken);
        var sumOfSquaredDifferences = data.Sum(x => (x - mean) * (x - mean));
        return sumOfSquaredDifferences / data.Count;
    }

    public Task<decimal> ComputeAverageAsync(IReadOnlyList<decimal> data, CancellationToken cancellationToken)
    {
        if (data.Count == 0) throw new ArgumentException("Data cannot be empty.", nameof(data));

        return Task.FromResult(data.Average());
    }
}