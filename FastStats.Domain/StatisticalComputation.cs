using FastStats.Domain.Strategies;
using FastStats.Domain.ValueObjects;

namespace FastStats.Domain;

public sealed class StatisticalComputation
{
    private StatisticalComputation()
    {
    }

    public StatisticalComputation(Dataset dataset)
    {
        Id = Guid.CreateVersion7();
        Dataset = dataset;
        ComputationStrategyIdentifier = ComputationStrategyIdentifier.None;
    }

    public Guid Id { get; init; }
    public Dataset Dataset { get; init; } = null!;
    public ComputationStrategyIdentifier ComputationStrategyIdentifier { get; private set; }
    public StatisticalSummary? Results { get; private set; }
    public DateTime? ComputedStartedAt { get; private set; }
    public DateTime? ComputedEndedAt { get; private set; }

    public async Task ComputeAsync(IComputationStrategy computationStrategy, CancellationToken cancellationToken)
    {
        ComputedStartedAt = DateTime.UtcNow;

        var medianTask = computationStrategy.ComputeMedianAsync(Dataset.Data, cancellationToken);
        var varianceTask = computationStrategy.ComputeVarianceAsync(Dataset.Data, cancellationToken);
        var averageTask = computationStrategy.ComputeAverageAsync(Dataset.Data, cancellationToken);

        ComputedEndedAt = DateTime.UtcNow;
        ComputationStrategyIdentifier = computationStrategy.Identifier;
        Results = new StatisticalSummary(await medianTask, await varianceTask, await averageTask);
    }
}