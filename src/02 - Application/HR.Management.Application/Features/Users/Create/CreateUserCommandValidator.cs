using FluentValidation;
using HR.Management.Domain.MessageResource;

namespace HR.Management.Application.Features.Users.Create;
public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private const int PASSWORD_MIN_LENGTH = 8;

    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name.Trim())
            .NotEmpty()
            .WithMessage(ErrorOnValidationMessageResource.NAME_REQUIRED);

        RuleFor(x => x.Email.Trim())
            .NotEmpty()
            .WithMessage(ErrorOnValidationMessageResource.EMAIL_REQUIRED)
            .EmailAddress()
            .WithMessage(ErrorOnValidationMessageResource.EMAIL_INVALID_FORMAT);

        RuleFor(x => x.Role.Trim())
            .NotEmpty()
            .WithMessage(ErrorOnValidationMessageResource.ROLE_REQUIRED);

        RuleFor(x => x.Password.Trim())
            .NotEmpty()
            .WithMessage(ErrorOnValidationMessageResource.PASSWORD_REQUIRED)
            .MinimumLength(PASSWORD_MIN_LENGTH)
            .WithMessage(ErrorOnValidationMessageResource.PASSWORD_MIN_LENGTH);
    }
}
