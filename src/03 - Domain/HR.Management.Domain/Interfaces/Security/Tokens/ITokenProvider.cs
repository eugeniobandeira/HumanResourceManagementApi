namespace HR.Management.Domain.Interfaces.Security.Tokens;

public interface ITokenProvider
{
    string TokenOnRequest();
}
