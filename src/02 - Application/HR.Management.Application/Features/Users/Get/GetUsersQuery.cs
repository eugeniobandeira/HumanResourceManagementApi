using HR.Management.Domain.Response;
using MediatR;

namespace HR.Management.Application.Features.Users.Get;

public sealed record GetUsersQuery : IRequest<IEnumerable<UserResponse>>;


