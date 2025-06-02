using FastStats.Api.Features.Computations.Models;
using FastStats.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;

namespace FastStats.Api.Features.Computations;

public static class GetComputationById
{
    public static void MapGetComputationById(this IEndpointRouteBuilder endpointRouteBuilder, Func<Guid, string> buildComputationKey)
    {
        endpointRouteBuilder.MapGet("{computationId:guid}",
                async ([FromRoute] Guid computationId,
                    [FromServices] IStatisticalComputationRepository repository,
                    [FromServices] HybridCache hybridCache,
                    CancellationToken cancellationToken) =>
                {
                    var computationResult = await hybridCache.GetOrCreateAsync(
                        buildComputationKey(computationId),
                        async ct =>
                        {
                            var computation = await repository.FirstOrDefaultAsync(computationId, ct);
                            return computation is not null
                                ? ComputationResult.FromComputation(computation)
                                : null;
                        },
                        cancellationToken: cancellationToken
                    );
                    return computationResult is not null
                        ? Results.Ok(computationResult)
                        : Results.NotFound();
                })
            .WithName("GetComputationById")
            .WithSummary("Gets a computation by its ID.")
            .WithDescription("Retrieves a specific statistical computation if it exists. The result is cached.");
    }
}