using FastStats.Domain.Repositories;
using FastStats.Infrastructure.Persistence;
using FastStats.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace FastStats.Infrastructure;

public static class DependencyInjection
{
    public static void AddDatabaseInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var databaseConnectionString = builder.Configuration.GetConnectionString("database");

        builder.Services.AddDbContext<ApplicationDbContext>((_, options) =>
        {
            options.UseNpgsql(databaseConnectionString);
        });

        builder.Services.AddScoped<ApplicationDbContextInitializer>();

        builder.Services.AddScoped<IDatasetRepository, DatasetRepository>();
        builder.Services.AddScoped<IStatisticalComputationRepository, StatisticalComputationRepository>();
    }

    public static void AddCacheInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.AddRedisClient("redis");
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = "FastStats:";
            options.ConnectionMultiplexerFactory = () => Task.FromResult(builder.Services.BuildServiceProvider()
                .GetRequiredService<IConnectionMultiplexer>());
        });

        builder.Services.AddHybridCache(options =>
        {
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromSeconds(15),
                Flags = HybridCacheEntryFlags.DisableLocalCache
            };
        });
    }
}