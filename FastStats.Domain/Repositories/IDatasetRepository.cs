namespace FastStats.Domain.Repositories;

public interface IDatasetRepository
{
    Task<Dataset?> FirstOrDefaultAsync(Guid datasetId, CancellationToken cancellationToken);
}