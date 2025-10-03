using HR.Management.Domain.Entities;

namespace HR.Management.Domain.Interfaces.UserRepository;
public interface IGetByEmailOnlyUserRepository
{
    Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
