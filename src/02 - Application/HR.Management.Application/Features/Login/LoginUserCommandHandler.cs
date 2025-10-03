using HR.Management.Domain.Exception;
using HR.Management.Domain.Interfaces.Security.Cryptography;
using HR.Management.Domain.Interfaces.Security.Tokens;
using HR.Management.Domain.Interfaces.UserRepository;
using HR.Management.Domain.Response;
using MediatR;

namespace HR.Management.Application.Features.Login;

public class LoginUserCommandHandler(
    IGetByEmailOnlyUserRepository getByEmailOnlyUserRepository,
    IPasswordEncripter passwordEncripter,
    IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<LoginUserCommand, LoginResponse>
{
    private readonly IGetByEmailOnlyUserRepository _getByEmailOnlyUserRepository = getByEmailOnlyUserRepository;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _getByEmailOnlyUserRepository.GetByEmailAsync(request.Email, cancellationToken)
            ?? throw new InvalidOperationException("User email not found.");

        var isPasswordValid = _passwordEncripter.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new InvalidLoginException();
        }

        return new LoginResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            Token = _jwtTokenGenerator.Generate(user),
        };
    }
}
