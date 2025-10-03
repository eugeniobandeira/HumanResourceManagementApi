using HR.Management.Application.Adapter;
using HR.Management.Domain.Interfaces.UserRepository;
using HR.Management.Domain.Response;
using MediatR;

namespace HR.Management.Application.Features.Users.Get;

public class GetUsersQueryHandler(IGetOnlyUserRepository getOnlyUserRepository) 
    : IRequestHandler<GetUsersQuery, IEnumerable<UserResponse>>
{
    private readonly IGetOnlyUserRepository _getOnlyUserRepository = getOnlyUserRepository;

    public async Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _getOnlyUserRepository.GetAllAsync(cancellationToken);

        if (!users.Any())
        {
            return [];
        }

        var responseList = new List<UserResponse>();

        foreach (var user in users)
        {
            var item = UserAdapter.FromEntityToResponse(user);
            responseList.Add(item);
        }

        return responseList;
    }
}
