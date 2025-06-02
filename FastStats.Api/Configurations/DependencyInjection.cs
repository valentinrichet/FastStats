using FastStats.Api.Services;

namespace FastStats.Api.Configurations;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddTransient<IComputationStrategyResolver, ComputationStrategyResolver>();
    }
}