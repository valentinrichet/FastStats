using BenchmarkDotNet.Attributes;
using FastStats.Domain.Strategies;

namespace FastStats.Domain.Benchmarks;

[MemoryDiagnoser]
public class ComputationBenchmark
{
    private Dataset _dataset = null!;
    private NaiveComputationStrategy _naiveStrategy = null!;
    private ParallelComputationStrategy _parallelStrategy = null!;
    private CancellationTokenSource _cts = null!;

    [Params(1000, 10000, 100000)] // Example data sizes
    public int DataSize;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var random = new Random(42); // Seed for reproducibility
        var data = Enumerable.Range(0, DataSize).Select(_ => (decimal)random.NextDouble() * 1000m).ToList();
        _dataset = new Dataset(data);
        _naiveStrategy = new NaiveComputationStrategy();
        _parallelStrategy = new ParallelComputationStrategy();
        _cts = new CancellationTokenSource();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        _cts?.Dispose();
    }

    [Benchmark(Baseline = true)]
    public async Task NaiveComputation()
    {
        var computation = new StatisticalComputation(_dataset);
        await computation.ComputeAsync(_naiveStrategy, _cts.Token);
    }

    [Benchmark]
    public async Task ParallelComputation()
    {
        var computation = new StatisticalComputation(_dataset);
        await computation.ComputeAsync(_parallelStrategy, _cts.Token);
    }
}