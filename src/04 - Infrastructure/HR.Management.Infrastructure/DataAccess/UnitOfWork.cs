using HR.Management.Domain.Interfaces;

namespace HR.Management.Infrastructure.DataAccess;
internal sealed class UnitOfWork(HrManagementDbContext hrManagementDbContext) : IUnitOfWork
{
    private readonly HrManagementDbContext _dbContext = hrManagementDbContext;

    public async Task Commit(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
