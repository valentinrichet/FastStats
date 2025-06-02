using FastStats.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FastStats.Infrastructure.Persistence.Configurations;

public sealed class ComputationStrategyIdentifierDbConverter : ValueConverter<ComputationStrategyIdentifier, string>
{
    private ComputationStrategyIdentifierDbConverter() : base(x => x.Name, x => new ComputationStrategyIdentifier(x))
    {
    }

    public static ComputationStrategyIdentifierDbConverter Instance { get; } = new();
}