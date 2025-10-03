using HR.Management.Domain.Interfaces.Security.Cryptography;

namespace HR.Management.Infrastructure.Security.Crypography;
internal class PasswordEncripter : IPasswordEncripter
{
    public string Encrypt(string password)
    {
        string  hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        return hashedPassword;
    }

    public bool Verify(string password, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(password, passwordHash);
}
