using System.Text.Json;
using FastStats.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastStats.Infrastructure.Persistence.Configurations;

public sealed class DatasetConfiguration : IEntityTypeConfiguration<Dataset>
{
    public void Configure(EntityTypeBuilder<Dataset> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property("_data")
            .HasColumnName("Data")
            .HasColumnType("jsonb")
            .HasConversion(new ValueConverter<decimal[], string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<decimal[]>(v, (JsonSerializerOptions?)null) ?? Array.Empty<decimal>()));
    }
}