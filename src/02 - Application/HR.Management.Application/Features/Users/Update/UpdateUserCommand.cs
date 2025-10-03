namespace HR.Management.Application.Features.Users.Update;
internal sealed record UpdateUserCommand(
    Guid Id,
    string Name,
    string Email,
    string Password
);
