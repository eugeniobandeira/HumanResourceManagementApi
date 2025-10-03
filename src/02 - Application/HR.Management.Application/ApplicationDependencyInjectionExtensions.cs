using FluentValidation;
using HR.Management.Application.Features.Users.Create;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Management.Application;
public static class ApplicationDependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjectionExtensions).Assembly));
    }
}
