using FastStats.Domain.Strategies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FastStats.Domain;

public static class DependencyInjection
{
    public static void AddDomainServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddTransient<IComputationStrategy, NaiveComputationStrategy>();
        builder.Services.AddTransient<IComputationStrategy, ParallelComputationStrategy>();
    }
}