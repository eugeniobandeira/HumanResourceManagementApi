using HR.Management.Application.Adapter;
using HR.Management.Domain.Interfaces.UserRepository;
using HR.Management.Domain.Response;
using MediatR;

namespace HR.Management.Application.Features.Users.GetById;
public class GetUserByIdQueryHandler(IGetByIdOnlyUserRepository getByIdOnlyUserRepository)
    : IRequestHandler<GetUserByIdQuery, UserResponse?>
{
    private readonly IGetByIdOnlyUserRepository _getByIdOnlyUserRepository = getByIdOnlyUserRepository;

    public async Task<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _getByIdOnlyUserRepository.GetByIdAsync(request.Id, cancellationToken);

        return user is null
            ? null
            : UserAdapter.FromEntityToResponse(user);
    }
}
