using FastStats.Api.Features.Computations.Models;
using FastStats.Domain.Strategies;

namespace FastStats.Api.Services;

public interface IComputationStrategyResolver
{
    IComputationStrategy? Resolve(ComputationStrategies strategy);
}