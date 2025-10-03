using HR.Management.Domain.Entities;

namespace HR.Management.Domain.Interfaces.Services;
public interface ILoggedUser
{
    Task<UserEntity> GetAsync(CancellationToken cancellationToken = default);
}
