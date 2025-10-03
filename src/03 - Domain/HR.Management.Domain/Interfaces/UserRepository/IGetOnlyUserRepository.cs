using HR.Management.Domain.Entities;

namespace HR.Management.Domain.Interfaces.UserRepository;
public interface IGetOnlyUserRepository
{
    Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}
