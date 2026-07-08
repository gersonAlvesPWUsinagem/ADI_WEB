using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.Encodings.Web;

namespace ADI_WEB.Security;

/// <summary>
/// Authenticates requests using a raw JWT stored in a cookie.
/// </summary>
public sealed class JwtCookieAuthenticationHandler : AuthenticationHandler<JwtAuthenticationOptions>
{
    private readonly JwtPrincipalFactory _principalFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtCookieAuthenticationHandler"/> class.
    /// </summary>
    public JwtCookieAuthenticationHandler(
        IOptionsMonitor<JwtAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        JwtPrincipalFactory principalFactory)
        : base(options, logger, encoder)
    {
        _principalFactory = principalFactory;
    }

    /// <inheritdoc />
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Cookies.TryGetValue(Options.CookieName, out var token) || string.IsNullOrWhiteSpace(token))
        {
            Logger.LogInformation("Cookie inexistente: {CookieName}", Options.CookieName);
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        Logger.LogInformation("Cookie encontrado: {CookieName}", Options.CookieName);

        try
        {
            var principal = _principalFactory.CreatePrincipal(token);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            Logger.LogInformation("JWT valido. Usuario autenticado: {UserName}", principal.Identity?.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
        catch (SecurityTokenExpiredException ex)
        {
            Logger.LogWarning(ex, "JWT expirado");
            return Task.FromResult(AuthenticateResult.Fail("JWT expirado"));
        }
        catch (SecurityTokenInvalidSignatureException ex)
        {
            Logger.LogWarning(ex, "Erro de assinatura do JWT");
            return Task.FromResult(AuthenticateResult.Fail("Erro de assinatura do JWT"));
        }
        catch (SecurityTokenException ex)
        {
            Logger.LogWarning(ex, "JWT invalido");
            return Task.FromResult(AuthenticateResult.Fail("JWT invalido"));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Erro de validacao do JWT");
            return Task.FromResult(AuthenticateResult.Fail("Erro de validacao do JWT"));
        }
    }

    /// <inheritdoc />
    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        var returnUrl = Request.PathBase + Request.Path + Request.QueryString;
        var loginPath = Options.LoginPath.HasValue ? Options.LoginPath.Value : JwtAuthenticationDefaults.LoginPath;
        var redirectUri = $"{loginPath}?returnUrl={Uri.EscapeDataString(returnUrl)}";

        Logger.LogInformation("Usuario nao autenticado. Redirecionando para {LoginPath}", loginPath);
        Response.Redirect(redirectUri);
        return Task.CompletedTask;
    }
}
