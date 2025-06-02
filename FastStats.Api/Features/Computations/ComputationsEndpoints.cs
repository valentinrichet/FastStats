using FastStats.Api.Features.Computations.Models;
using FastStats.Api.Services;
using FastStats.Domain.Repositories;
using FastStats.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;

namespace FastStats.Api.Features.Computations;

public static class ComputationsEndpoints
{
    public static void MapComputationsEndpoints(this WebApplication app)
    {
        const string basePath = "/api/v1/computations";

        var computations = app.MapGroup(basePath)
            .WithTags("Computations");

        computations.MapGetComputationById(BuildComputationKey);
        computations.MapStatisticalCompute(basePath, BuildComputationKey);
        return;

        static string BuildComputationKey(Guid id)
        {
            return $"computations/{id}";
        }
    }
}