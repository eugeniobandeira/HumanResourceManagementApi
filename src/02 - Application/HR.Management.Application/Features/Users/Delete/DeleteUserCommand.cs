using MediatR;

namespace HR.Management.Application.Features.Users.Delete;

public sealed record DeleteUserCommand(Guid Id) : IRequest;
