using HR.Management.Application.Features.Users.Create;
using HR.Management.Domain.Entities;
using HR.Management.Domain.Response;

namespace HR.Management.Application.Adapter;
public static class UserAdapter
{
    public static UserEntity FromCommandToEntity(CreateUserCommand command, string hashedPassword)
    {
        return new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Email = command.Email,
            PasswordHash = hashedPassword,
            Role = command.Role
        };
    }

    public static UserResponse FromEntityToResponse(UserEntity user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role
        };
    }
}
