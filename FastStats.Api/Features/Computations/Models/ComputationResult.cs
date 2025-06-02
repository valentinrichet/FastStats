using FastStats.Domain;

namespace FastStats.Api.Features.Computations.Models;

public sealed record ComputationResult(
    Guid ComputationId,
    Guid DatasetId,
    string Strategy,
    StatisticalSummaryResult StatisticalSummary,
    DateTime ComputedStartedAt,
    DateTime ComputedEndedAt)
{
    public static ComputationResult FromComputation(StatisticalComputation statisticalComputation)
    {
        return new ComputationResult(
            statisticalComputation.Id,
            statisticalComputation.Dataset.Id,
            statisticalComputation.ComputationStrategyIdentifier.Name,
            statisticalComputation.Results is not null
                ? StatisticalSummaryResult.FromStatisticalSummary(statisticalComputation.Results)
                : StatisticalSummaryResult.Empty,
            statisticalComputation.ComputedStartedAt ?? DateTime.MinValue,
            statisticalComputation.ComputedEndedAt ?? DateTime.MinValue);
    }
}