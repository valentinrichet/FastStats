using FastStats.Domain.ValueObjects;

namespace FastStats.Api.Features.Computations.Models;

public sealed record StatisticalSummaryResult(decimal Median, decimal Variance, decimal Average)
{
    public static readonly StatisticalSummaryResult Empty = new(0, 0, 0);

    public static StatisticalSummaryResult FromStatisticalSummary(StatisticalSummary statisticalSummary)
    {
        return new StatisticalSummaryResult(statisticalSummary.Median, statisticalSummary.Variance,
            statisticalSummary.Average);
    }
}