using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace ADI_WEB.Security;

/// <summary>
/// Coordinates login, logout and authentication state based on the JWT cookie.
/// </summary>
public sealed class JwtAuthenticationService
{
    private readonly ICookieService _cookieService;
    private readonly JwtPrincipalFactory _principalFactory;
    private readonly JwtTokenNormalizer _tokenNormalizer;
    private readonly ILogger<JwtAuthenticationService> _logger;

    /// <summary>
    /// Occurs when authentication state changes in the current Blazor circuit.
    /// </summary>
    public event Func<ClaimsPrincipal, Task>? AuthenticationStateChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtAuthenticationService"/> class.
    /// </summary>
    public JwtAuthenticationService(
        ICookieService cookieService,
        JwtPrincipalFactory principalFactory,
        JwtTokenNormalizer tokenNormalizer,
        ILogger<JwtAuthenticationService> logger)
    {
        _cookieService = cookieService;
        _principalFactory = principalFactory;
        _tokenNormalizer = tokenNormalizer;
        _logger = logger;
    }

    /// <summary>
    /// Stores the JWT and authenticates the current Blazor circuit.
    /// </summary>
    public async Task LoginAsync(string token)
    {
        var normalizedToken = _tokenNormalizer.Normalize(token);

        await _cookieService.SetAuthTokenAsync(normalizedToken);
        var principal = _principalFactory.CreatePrincipal(normalizedToken);
        _logger.LogInformation("Usuario autenticado: {UserName}", principal.Identity?.Name);
        await NotifyAuthenticationStateChangedAsync(principal);
    }

    /// <summary>
    /// Removes the JWT and marks the current Blazor circuit as anonymous.
    /// </summary>
    public async Task LogoutAsync()
    {
        await _cookieService.RemoveAuthTokenAsync();
        _logger.LogInformation("Usuario nao autenticado");
        await NotifyAuthenticationStateChangedAsync(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    /// <summary>
    /// Gets the current JWT from the authentication cookie.
    /// </summary>
    public Task<string?> GetTokenAsync()
    {
        return _cookieService.GetAuthTokenAsync();
    }

    /// <summary>
    /// Returns whether the current token is authenticated.
    /// </summary>
    public async Task<bool> IsAuthenticatedAsync()
    {
        return (await GetPrincipalAsync()).Identity?.IsAuthenticated == true;
    }

    /// <summary>
    /// Gets the current principal from the JWT cookie.
    /// </summary>
    public async Task<ClaimsPrincipal> GetPrincipalAsync()
    {
        var token = await _cookieService.GetAuthTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
        {
            _logger.LogInformation("Cookie inexistente");
            return new ClaimsPrincipal(new ClaimsIdentity());
        }

        try
        {
            var principal = _principalFactory.CreatePrincipal(token);
            _logger.LogInformation("JWT valido");
            return principal;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "JWT invalido");
            return new ClaimsPrincipal(new ClaimsIdentity());
        }
    }

    private async Task NotifyAuthenticationStateChangedAsync(ClaimsPrincipal principal)
    {
        if (AuthenticationStateChanged is not null)
        {
            await AuthenticationStateChanged.Invoke(principal);
        }
    }
}
