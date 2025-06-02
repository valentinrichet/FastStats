using FastStats.Domain.ValueObjects;

namespace FastStats.Domain.Strategies;

public interface IComputationStrategy
{
    ComputationStrategyIdentifier Identifier { get; }
    Task<decimal> ComputeMedianAsync(IReadOnlyList<decimal> data, CancellationToken cancellationToken);
    Task<decimal> ComputeVarianceAsync(IReadOnlyList<decimal> data, CancellationToken cancellationToken);
    Task<decimal> ComputeAverageAsync(IReadOnlyList<decimal> data, CancellationToken cancellationToken);
}