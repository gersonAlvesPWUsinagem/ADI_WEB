using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace ADI_WEB.Security;

/// <summary>
/// Supplies authentication state to Blazor from the JWT authentication service.
/// </summary>
public sealed class JwtAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
{
    private readonly JwtAuthenticationService _jwtAuthenticationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtAuthenticationStateProvider"/> class.
    /// </summary>
    public JwtAuthenticationStateProvider(JwtAuthenticationService jwtAuthenticationService)
    {
        _jwtAuthenticationService = jwtAuthenticationService;
        _jwtAuthenticationService.AuthenticationStateChanged += OnAuthenticationStateChanged;
    }

    /// <inheritdoc />
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(await _jwtAuthenticationService.GetPrincipalAsync());
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _jwtAuthenticationService.AuthenticationStateChanged -= OnAuthenticationStateChanged;
    }

    private Task OnAuthenticationStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        return Task.CompletedTask;
    }
}
