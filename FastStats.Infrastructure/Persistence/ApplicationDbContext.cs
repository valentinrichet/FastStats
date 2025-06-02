using System.Reflection;
using FastStats.Domain;
using Microsoft.EntityFrameworkCore;

namespace FastStats.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Dataset> Datasets => Set<Dataset>();
    public DbSet<StatisticalComputation> StatisticalComputations => Set<StatisticalComputation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}