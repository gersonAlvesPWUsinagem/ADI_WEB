using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace ADI_WEB.Security;

/// <summary>
/// Configures the JWT cookie authentication scheme.
/// </summary>
public sealed class JwtAuthenticationOptions : AuthenticationSchemeOptions
{
    /// <summary>
    /// Gets or sets the cookie name that stores the JWT.
    /// </summary>
    public string CookieName { get; set; } = JwtAuthenticationDefaults.CookieName;

    /// <summary>
    /// Gets or sets the login path used by authorization redirects.
    /// </summary>
    public PathString LoginPath { get; set; } = JwtAuthenticationDefaults.LoginPath;

    /// <summary>
    /// Gets or sets the logout path.
    /// </summary>
    public PathString LogoutPath { get; set; } = JwtAuthenticationDefaults.LogoutPath;

    /// <summary>
    /// Gets or sets a value indicating whether the cookie should renew near expiration.
    /// </summary>
    public bool SlidingExpiration { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the JWT lifetime must be validated.
    /// </summary>
    public bool ValidateLifetime { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether the issuer must be validated.
    /// </summary>
    public bool ValidateIssuer { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the audience must be validated.
    /// </summary>
    public bool ValidateAudience { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the signing key must be validated.
    /// </summary>
    public bool ValidateSigningKey { get; set; }

    /// <summary>
    /// Gets or sets the signing key used to validate JWT signatures.
    /// </summary>
    public string? SigningKey { get; set; }

    /// <summary>
    /// Gets or sets the expected token issuer.
    /// </summary>
    public string? Issuer { get; set; }

    /// <summary>
    /// Gets or sets the expected token audience.
    /// </summary>
    public string? Audience { get; set; }

    /// <summary>
    /// Gets or sets the clock skew used during lifetime validation.
    /// </summary>
    public TimeSpan ClockSkew { get; set; } = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Gets or sets the SameSite mode applied when writing the cookie through JavaScript.
    /// </summary>
    public SameSiteMode SameSite { get; set; } = SameSiteMode.Strict;

    /// <summary>
    /// Gets or sets a value indicating whether the cookie should be HTTP only.
    /// </summary>
    public bool HttpOnly { get; set; }

    /// <summary>
    /// Gets or sets the secure policy applied when writing the cookie through JavaScript.
    /// </summary>
    public CookieSecurePolicy SecurePolicy { get; set; } = CookieSecurePolicy.Always;

    /// <summary>
    /// Gets or sets the cookie path.
    /// </summary>
    public string CookiePath { get; set; } = JwtAuthenticationDefaults.CookiePath;
}
