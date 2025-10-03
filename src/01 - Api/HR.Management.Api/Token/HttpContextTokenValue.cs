using HR.Management.Domain.Interfaces.Security.Tokens;

namespace HR.Management.Api.Token;

/// <summary>
/// Provides a token value extracted from the current HTTP request's Authorization header.
/// </summary>
/// <remarks>This class retrieves the token from the Authorization header of the current HTTP context. It assumes
/// the token is prefixed with "Bearer " and trims the prefix to return the token value.</remarks>
/// <param name="httpContextAccessor"></param>
public class HttpContextTokenValue(IHttpContextAccessor httpContextAccessor) : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <summary>
    /// Extracts the token from the "Authorization" header of the current HTTP request.
    /// </summary>
    /// <remarks>This method assumes that the "Authorization" header contains a Bearer token.  The token is
    /// extracted by removing the "Bearer " prefix and trimming any surrounding whitespace.</remarks>
    /// <returns>The token string extracted from the "Authorization" header.</returns>
    public string TokenOnRequest()
    {
        var authorizationHeader = _httpContextAccessor
            .HttpContext!
            .Request
            .Headers.Authorization.ToString();

        return authorizationHeader["Bearer ".Length..].Trim();
    }
}
