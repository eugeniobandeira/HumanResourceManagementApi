using HR.Management.Domain.Response;
using MediatR;

namespace HR.Management.Application.Features.Users.GetById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserResponse?>;
