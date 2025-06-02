namespace FastStats.Domain.Repositories;

public interface IStatisticalComputationRepository
{
    Task<StatisticalComputation?> FirstOrDefaultAsync(Guid statisticalComputationId,
        CancellationToken cancellationToken);

    Task AddAsync(StatisticalComputation statisticalComputation, CancellationToken cancellationToken);
}