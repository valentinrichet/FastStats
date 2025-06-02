using FastStats.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;

namespace FastStats.Api.Tests;

public sealed class FastStatsApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer =
        new PostgreSqlBuilder()
            .WithImage("postgres:17.4-alpine")
            .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .WithImage("redis:7.4-alpine")
        .Build();

    public async Task InitializeAsync()
    {
        await Task.WhenAll(_dbContainer.StartAsync(), _redisContainer.StartAsync());
    }

    public new async Task DisposeAsync()
    {
        await Task.WhenAll(_dbContainer.DisposeAsync().AsTask(), _redisContainer.DisposeAsync().AsTask());
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });
            services.RemoveAll(typeof(IConnectionMultiplexer));
            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(_redisContainer.GetConnectionString()));
        });
    }
}