using FastStats.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FastStats.AppHost;

public sealed class DataContextDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder = DistributedApplication.CreateBuilder(args);

        const string databaseName = "migrations";

        var postgres = builder
            .AddPostgres("postgres")
            .AddDatabase(databaseName, databaseName);

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(databaseName);
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}