using FastStats.Domain;
using FastStats.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FastStats.Infrastructure.Persistence.Repositories;

public sealed class DatasetRepository : IDatasetRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DatasetRepository(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }

    public async Task<Dataset?> FirstOrDefaultAsync(Guid datasetId, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<Dataset>()
            .FirstOrDefaultAsync(d => d.Id == datasetId, cancellationToken);
    }
}