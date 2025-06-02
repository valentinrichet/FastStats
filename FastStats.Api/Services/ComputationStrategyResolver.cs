using FastStats.Api.Features.Computations.Models;
using FastStats.Domain.Strategies;
using FastStats.Domain.ValueObjects;

namespace FastStats.Api.Services;

public sealed class ComputationStrategyResolver : IComputationStrategyResolver
{
    private readonly IEnumerable<IComputationStrategy> _strategies;

    public ComputationStrategyResolver(IEnumerable<IComputationStrategy> strategies)
    {
        _strategies = strategies;
    }

    public IComputationStrategy? Resolve(ComputationStrategies strategy)
    {
        var identifier = strategy switch
        {
            ComputationStrategies.Naive => ComputationStrategyIdentifier.Naive,
            ComputationStrategies.Parallel => ComputationStrategyIdentifier.Parallel,
            _ => ComputationStrategyIdentifier.None
        };
        return identifier != ComputationStrategyIdentifier.None
            ? _strategies.FirstOrDefault(cs => cs.Identifier == identifier)
            : null;
    }
}