using HR.Management.Domain.Entities;
using HR.Management.Domain.Interfaces.Security.Tokens;
using HR.Management.Domain.Interfaces.Services;
using HR.Management.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HR.Management.Infrastructure.Services.LoggedUser;

internal class LoggedUser(
    HrManagementDbContext hrManagementDbContext, 
    ITokenProvider tokenProvider) 
    : ILoggedUser
{
    private readonly HrManagementDbContext _dbContext = hrManagementDbContext;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    public async Task<UserEntity> GetAsync(CancellationToken cancellationToken = default)
    {
        string token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.Id == Guid.Parse(identifier), cancellationToken);
    }
}
