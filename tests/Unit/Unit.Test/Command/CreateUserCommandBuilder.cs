using Bogus;
using HR.Management.Application.Features.Users.Create;

namespace Unit.Test.Command;
public static class CreateUserCommandBuilder
{
    private static readonly string[] items = ["Admin", "Regular"];

    public static CreateUserCommand Build()
    {
        return new Faker<CreateUserCommand>()
            .CustomInstantiator(f => new CreateUserCommand(
                Name: f.Name.FullName(),
                Email: f.Internet.Email(),
                Role: f.PickRandom(items),
                Password: f.Internet.Password(8)
            ))
            .Generate();
    }
}
