using HR.Management.Domain.Entities;

namespace HR.Management.Domain.Interfaces.Security.Tokens;

public interface IJwtTokenGenerator
{
    string Generate(UserEntity user);
}
