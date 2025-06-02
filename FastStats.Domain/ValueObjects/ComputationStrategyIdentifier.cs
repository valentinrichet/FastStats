namespace FastStats.Domain.ValueObjects;

public readonly record struct ComputationStrategyIdentifier
{
    public static readonly ComputationStrategyIdentifier None = new("None");
    public static readonly ComputationStrategyIdentifier Naive = new("Naive");
    public static readonly ComputationStrategyIdentifier Parallel = new("Parallel");

    public ComputationStrategyIdentifier()
    {
        Name = None.Name;
    }

    public ComputationStrategyIdentifier(string strategyName)
    {
        Name = strategyName;
    }

    public string Name { get; init; } = "None";
}