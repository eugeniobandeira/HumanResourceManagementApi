using HR.Management.Domain.Entities;

namespace HR.Management.Domain.Interfaces.UserRepository;
public interface IGetByIdOnlyUserRepository
{
    Task<UserEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
