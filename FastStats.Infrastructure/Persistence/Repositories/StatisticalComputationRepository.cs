using FastStats.Domain;
using FastStats.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FastStats.Infrastructure.Persistence.Repositories;

public sealed class StatisticalComputationRepository : IStatisticalComputationRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StatisticalComputationRepository(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }

    public Task<StatisticalComputation?> FirstOrDefaultAsync(Guid statisticalComputationId,
        CancellationToken cancellationToken)
    {
        return _dbContext
            .Set<StatisticalComputation>()
            .AsNoTracking()
            .Include(c => c.Dataset)
            .FirstOrDefaultAsync(c => c.Id == statisticalComputationId, cancellationToken);
    }

    public async Task AddAsync(StatisticalComputation statisticalComputation, CancellationToken cancellationToken)
    {
        await _dbContext
            .Set<StatisticalComputation>()
            .AddAsync(statisticalComputation, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}