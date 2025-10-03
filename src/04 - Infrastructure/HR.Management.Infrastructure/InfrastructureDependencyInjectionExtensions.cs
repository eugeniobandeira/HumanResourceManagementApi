using HR.Management.Domain.Interfaces;
using HR.Management.Domain.Interfaces.Security.Cryptography;
using HR.Management.Domain.Interfaces.Security.Tokens;
using HR.Management.Domain.Interfaces.UserRepository;
using HR.Management.Infrastructure.DataAccess;
using HR.Management.Infrastructure.DataAccess.Repositories;
using HR.Management.Infrastructure.Security.Crypography;
using HR.Management.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Management.Infrastructure;
public static class InfrastructureDependencyInjectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncripter, PasswordEncripter>();

        AddToken(services, configuration);
        AddRepositories(services);
        AddDbContext(services, configuration);
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration["Settings:Jwt:ExpiresMinutes"];

        uint expirationTimeMinutesUint = uint.Parse(expirationTimeMinutes!);

        var signingKey = configuration["Settings:Jwt:SigningKey"];

        services.AddScoped<IJwtTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutesUint, signingKey!));
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("HrManagementConnection");

        services.AddDbContext<HrManagementDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAddOnlyUserRepository, UserRepository>();
        services.AddScoped<IGetOnlyUserRepository, UserRepository>();
        services.AddScoped<IGetByIdOnlyUserRepository, UserRepository>();
        services.AddScoped<IGetByEmailOnlyUserRepository, UserRepository>();
    }
}
