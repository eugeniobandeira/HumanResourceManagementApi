using HR.Management.Domain.Response;
using MediatR;

namespace HR.Management.Application.Features.Users.Create
{
    public sealed record CreateUserCommand(
        string Name,
        string Email,
        string Role,
        string Password
    ) : IRequest<UserResponse>;
}
