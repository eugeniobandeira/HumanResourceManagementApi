using HR.Management.Domain.Entities;
using HR.Management.Domain.Interfaces.UserRepository;
using Microsoft.EntityFrameworkCore;

namespace HR.Management.Infrastructure.DataAccess.Repositories;
internal class UserRepository(HrManagementDbContext dbContext) : 
    IAddOnlyUserRepository,
    IGetOnlyUserRepository,
    IGetByIdOnlyUserRepository,
    IGetByEmailOnlyUserRepository
{
    private readonly HrManagementDbContext _dbContext = dbContext;

    public async Task AddUserAsync(
        UserEntity user, 
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.AsNoTracking().ToListAsync();
    }

    public async Task<UserEntity?> GetByEmailAsync(
        string email, 
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(user => user.Email.Equals(email), cancellationToken);
    }

    public async Task<UserEntity?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id.Equals(id), cancellationToken);
    }
}
