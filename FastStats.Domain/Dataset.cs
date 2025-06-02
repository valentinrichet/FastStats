namespace FastStats.Domain;

public sealed class Dataset
{
    private readonly decimal[] _data = null!;

    private Dataset()
    {
    }

    public Dataset(IEnumerable<decimal> data)
    {
        Id = Guid.CreateVersion7();
        _data = data.ToArray();
        if (_data.Length is 0) throw new ArgumentException("Dataset cannot be empty.", nameof(data));
    }

    public Guid Id { get; init; }
    public IReadOnlyList<decimal> Data => _data.AsReadOnly();
}