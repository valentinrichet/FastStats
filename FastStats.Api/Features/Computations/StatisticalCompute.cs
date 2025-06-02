using FastStats.Api.Features.Computations.Models;
using FastStats.Api.Services;
using FastStats.Domain.Repositories;
using FastStats.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;

namespace FastStats.Api.Features.Computations;

public static class StatisticalCompute
{
    public static void MapStatisticalCompute(
        this IEndpointRouteBuilder endpointRouteBuilder,
        string basePath,
        Func<Guid, string> buildComputationKey)
    {
        endpointRouteBuilder.MapPost("",
                async ([FromBody] ComputationRequest request,
                    [FromServices] IComputationStrategyResolver computationStrategyResolver,
                    [FromServices] IStatisticalComputationRepository repository,
                    [FromServices] HybridCache hybridCache,
                    CancellationToken cancellationToken) =>
                {
                    var computationStrategy = computationStrategyResolver.Resolve(request.Strategy);
                    if (computationStrategy is null ||
                        computationStrategy.Identifier == ComputationStrategyIdentifier.None)
                        return Results.BadRequest("Invalid computation strategy.");

                    var computation = request.ToComputation();
                    await computation.ComputeAsync(computationStrategy, cancellationToken);
                    await repository.AddAsync(computation, cancellationToken);
                    var computationResult = ComputationResult.FromComputation(computation);
                    await hybridCache.SetAsync(buildComputationKey(computation.Id), computationResult,
                        cancellationToken: cancellationToken);
                    return Results.Created($"{basePath}/{computation.Id}", computationResult);
                })
            .WithName("CreateComputation")
            .WithSummary("Creates a new statistical computation.")
            .WithDescription(
                "Performs a statistical computation based on the provided strategy and data, then stores and caches the result.");
    }
}