using FastStats.Domain.ValueObjects;

namespace FastStats.Domain.Tests.ValueObjects;

public class ComputationStrategyIdentifierTests
{
    [Fact]
    public void None_HasCorrectName()
    {
        // Assert
        Assert.Equal("None", ComputationStrategyIdentifier.None.Name);
    }

    [Fact]
    public void Naive_HasCorrectName()
    {
        // Assert
        Assert.Equal("Naive", ComputationStrategyIdentifier.Naive.Name);
    }

    [Fact]
    public void Parallel_HasCorrectName()
    {
        // Assert
        Assert.Equal("Parallel", ComputationStrategyIdentifier.Parallel.Name);
    }

    [Fact]
    public void Constructor_AssignsNameCorrectly()
    {
        // Arrange
        var strategyName = "CustomStrategy";

        // Act
        var identifier = new ComputationStrategyIdentifier(strategyName);

        // Assert
        Assert.Equal(strategyName, identifier.Name);
    }

    [Fact]
    public void Constructor_Default_AssignsEmptyStringToName()
    {
        // Act
        var identifier = new ComputationStrategyIdentifier();

        // Assert
        Assert.Equal(ComputationStrategyIdentifier.None, identifier);
    }

    [Fact]
    public void Equality_SameName_ReturnsTrue()
    {
        // Arrange
        var name = "StrategyA";
        var id1 = new ComputationStrategyIdentifier(name);
        var id2 = new ComputationStrategyIdentifier(name);

        // Act & Assert
        Assert.True(id1.Equals(id2));
        Assert.True(id1 == id2);
        Assert.False(id1 != id2);
        Assert.Equal(id1.GetHashCode(), id2.GetHashCode());
    }

    [Fact]
    public void Equality_DifferentName_ReturnsFalse()
    {
        // Arrange
        var id1 = new ComputationStrategyIdentifier("StrategyA");
        var id2 = new ComputationStrategyIdentifier("StrategyB");

        // Act & Assert
        Assert.False(id1.Equals(id2));
        Assert.False(id1 == id2);
        Assert.True(id1 != id2);
        Assert.NotEqual(id1.GetHashCode(), id2.GetHashCode());
    }

    [Fact]
    public void Equality_WithStaticInstances_BehavesCorrectly()
    {
        // Arrange
        var naiveInstance = new ComputationStrategyIdentifier("Naive");

        // Act & Assert
        Assert.True(ComputationStrategyIdentifier.Naive.Equals(naiveInstance));
        Assert.True(ComputationStrategyIdentifier.Naive == naiveInstance);
        Assert.False(ComputationStrategyIdentifier.Naive != naiveInstance);
        Assert.Equal(ComputationStrategyIdentifier.Naive.GetHashCode(), naiveInstance.GetHashCode());

        Assert.False(ComputationStrategyIdentifier.Naive.Equals(ComputationStrategyIdentifier.Parallel));
        Assert.False(ComputationStrategyIdentifier.Naive == ComputationStrategyIdentifier.Parallel);
        Assert.True(ComputationStrategyIdentifier.Naive != ComputationStrategyIdentifier.Parallel);
    }

    [Fact]
    public void Equality_ComparingWithDifferentType_ReturnsFalse()
    {
        // Arrange
        var identifier = ComputationStrategyIdentifier.Naive;
        var otherObject = new object();

        // Act & Assert
        Assert.False(identifier.Equals(otherObject));
    }
}