namespace ADI_WEB.Security;

/// <summary>
/// Provides centralized access to authentication cookies.
/// </summary>
public interface ICookieService
{
    /// <summary>
    /// Writes the JWT authentication cookie.
    /// </summary>
    Task SetAuthTokenAsync(string token);

    /// <summary>
    /// Reads the JWT authentication cookie.
    /// </summary>
    Task<string?> GetAuthTokenAsync();

    /// <summary>
    /// Removes the JWT authentication cookie.
    /// </summary>
    Task RemoveAuthTokenAsync();
}
