using HR.Management.Domain.Response;
using MediatR;

namespace HR.Management.Application.Features.Login;

public sealed record LoginUserCommand(
    string Email,
    string Password) : IRequest<LoginResponse>;
