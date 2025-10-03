using FluentValidation;
using HR.Management.Application.Adapter;
using HR.Management.Domain.Exception;
using HR.Management.Domain.Interfaces;
using HR.Management.Domain.Interfaces.Security.Cryptography;
using HR.Management.Domain.Interfaces.UserRepository;
using HR.Management.Domain.Response;
using MediatR;

namespace HR.Management.Application.Features.Users.Create;

public sealed class CreateUserCommandHandler(
    IValidator<CreateUserCommand> validator,
    IAddOnlyUserRepository writeOnlyUserRepository,
    IGetByEmailOnlyUserRepository getByEmailOnlyUserRepository,
    IPasswordEncripter passwordEncripter,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly IValidator<CreateUserCommand> _validator = validator;
    private readonly IAddOnlyUserRepository _writeOnlyUserRepository = writeOnlyUserRepository;
    private readonly IGetByEmailOnlyUserRepository _getByEmailOnlyUserRepository = getByEmailOnlyUserRepository;
    private readonly IPasswordEncripter _passwordEncripter = passwordEncripter;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    async Task<UserResponse> IRequestHandler<CreateUserCommand, UserResponse>.Handle(
        CreateUserCommand request, CancellationToken cancellationToken)
    {
        await ValidateCommandAsync(request, cancellationToken);
        await ValidateEmailUniquenessAsync(request.Email, cancellationToken);

        var hashedPassword = _passwordEncripter.Encrypt(request.Password);

        var user = UserAdapter.FromCommandToEntity(request, hashedPassword);

        await _writeOnlyUserRepository.AddUserAsync(user, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);

        return UserAdapter.FromEntityToResponse(user);
    }

    private async Task ValidateCommandAsync(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }

    private async Task ValidateEmailUniquenessAsync(string email, CancellationToken cancellationToken)
    {
        var existingUser = await _getByEmailOnlyUserRepository.GetByEmailAsync(email, cancellationToken);

        if (existingUser is not null)
        {
            throw new ErrorOnValidationException([ErrorMessageResource.EMAIL_ALREADY_REGISTERED]);
        }
    }
}
