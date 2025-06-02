using FastStats.Domain;

namespace FastStats.Api.Features.Computations.Models;

public sealed record ComputationRequest(IReadOnlyList<decimal> Data, ComputationStrategies Strategy)
{
    public StatisticalComputation ToComputation()
    {
        var dataset = new Dataset(Data);
        return new StatisticalComputation(dataset);
    }
}