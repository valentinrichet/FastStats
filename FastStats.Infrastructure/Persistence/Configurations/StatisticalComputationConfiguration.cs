using FastStats.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastStats.Infrastructure.Persistence.Configurations;

public sealed class StatisticalComputationConfiguration : IEntityTypeConfiguration<StatisticalComputation>
{
    public void Configure(EntityTypeBuilder<StatisticalComputation> builder)
    {
        builder.HasKey(sc => sc.Id);

        builder.HasOne(sc => sc.Dataset)
            .WithMany()
            .IsRequired();

        builder.Property(sc => sc.ComputationStrategyIdentifier)
            .HasConversion(ComputationStrategyIdentifierDbConverter.Instance)
            .HasMaxLength(50);

        builder.OwnsOne(sc => sc.Results, resultsBuilder =>
        {
            resultsBuilder.Property(r => r.Median)
                .HasColumnName("Median");

            resultsBuilder.Property(r => r.Variance)
                .HasColumnName("Variance");

            resultsBuilder.Property(r => r.Average)
                .HasColumnName("Average");
        });
    }
}