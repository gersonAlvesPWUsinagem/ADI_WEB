namespace ADI_WEB.Security;

/// <summary>
/// Centralizes default values used by the JWT cookie authentication infrastructure.
/// </summary>
public static class JwtAuthenticationDefaults
{
    /// <summary>
    /// Default authentication scheme name.
    /// </summary>
    public const string AuthenticationScheme = "AdiJwtCookie";

    /// <summary>
    /// Default cookie name that stores the JWT.
    /// </summary>
    public const string CookieName = "AuthTokenADI";

    /// <summary>
    /// Default login path.
    /// </summary>
    public const string LoginPath = "/user/login";

    /// <summary>
    /// Default logout path.
    /// </summary>
    public const string LogoutPath = "/user/logout";

    /// <summary>
    /// Default cookie path.
    /// </summary>
    public const string CookiePath = "/";
}
