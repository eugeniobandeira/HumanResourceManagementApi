namespace HR.Management.Domain.Response;
public sealed class LoginResponse : UserResponse
{
    public string Token { get; set; } = string.Empty;
}
