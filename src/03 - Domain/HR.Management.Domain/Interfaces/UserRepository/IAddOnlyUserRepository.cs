using HR.Management.Domain.Entities;

namespace HR.Management.Domain.Interfaces.UserRepository;
public interface IAddOnlyUserRepository
{
    Task AddUserAsync(UserEntity user, CancellationToken cancellationToken = default);
}
