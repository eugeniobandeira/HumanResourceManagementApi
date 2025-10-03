using HR.Management.Domain.Entities;
using HR.Management.Domain.Interfaces.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.Management.Infrastructure.Security.Tokens;
internal class JwtTokenGenerator(uint expiryMinutes, string signingKey)
    : IJwtTokenGenerator
{
    private readonly uint _expiryMinutes = expiryMinutes;
    private readonly string _signingKey = signingKey;

    public string Generate(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Sid, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expiryMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey)),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }
}
