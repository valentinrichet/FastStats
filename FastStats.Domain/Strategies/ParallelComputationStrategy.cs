using System.Collections.Immutable;
using FastStats.Domain.ValueObjects;

namespace FastStats.Domain.Strategies;

public sealed class ParallelComputationStrategy : IComputationStrategy
{
    public ComputationStrategyIdentifier Identifier => ComputationStrategyIdentifier.Parallel;

    public Task<decimal> ComputeMedianAsync(IReadOnlyList<decimal> data, CancellationToken cancellationToken)
    {
        if (data.Count == 0) throw new ArgumentException("Data cannot be empty.", nameof(data));

        var sortedData = data
            .AsParallel()
            .OrderBy(x => x)
            .ToImmutableArray();
        var median = data.Count % 2 == 1
            ? sortedData[data.Count / 2]
            : (sortedData[data.Count / 2 - 1] + sortedData[data.Count / 2]) / 2.0m;
        return Task.FromResult(median);
    }

    public async Task<decimal> ComputeVarianceAsync(IReadOnlyList<decimal> data, CancellationToken cancellationToken)
    {
        if (data.Count == 0) throw new ArgumentException("Data cannot be empty.", nameof(data));

        var mean = await ComputeAverageAsync(data, cancellationToken);
        var sumOfSquaredDifferences = data
            .AsParallel()
            .Sum(x => (x - mean) * (x - mean));
        return sumOfSquaredDifferences / data.Count;
    }

    public Task<decimal> ComputeAverageAsync(IReadOnlyList<decimal> data, CancellationToken cancellationToken)
    {
        if (data.Count == 0) throw new ArgumentException("Data cannot be empty.", nameof(data));

        var sum = data
            .AsParallel()
            .Sum();
        return Task.FromResult(sum / data.Count);
    }
}