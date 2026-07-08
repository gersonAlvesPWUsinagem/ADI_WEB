using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;

namespace ADI_WEB.Security;

/// <summary>
/// Registers JWT cookie authentication services.
/// </summary>
public static class JwtAuthenticationExtensions
{
    /// <summary>
    /// Adds the reusable JWT cookie authentication infrastructure.
    /// </summary>
    public static IServiceCollection AddJwtCookieAuthentication(
        this IServiceCollection services,
        Action<JwtAuthenticationOptions>? configureOptions = null)
    {
        services.Configure(configureOptions ?? (_ => { }));

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtAuthenticationDefaults.AuthenticationScheme;
            })
            .AddScheme<JwtAuthenticationOptions, JwtCookieAuthenticationHandler>(
                JwtAuthenticationDefaults.AuthenticationScheme,
                configureOptions ?? (_ => { }));

        services.AddScoped<JwtPrincipalFactory>();
        services.AddScoped<JwtTokenNormalizer>();
        services.AddScoped<AuthCookieJsInterop>();
        services.AddScoped<ICookieService, CookieService>();
        services.AddScoped<JwtAuthenticationService>();
        services.AddScoped<JwtAuthenticationStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<JwtAuthenticationStateProvider>());
        services.AddScoped<ICurrentUser, CurrentUserService>();
        services.AddSingleton<IConfigureOptions<JwtAuthenticationOptions>, JwtAuthenticationPostConfigureOptions>();

        return services;
    }

    private sealed class JwtAuthenticationPostConfigureOptions : IConfigureOptions<JwtAuthenticationOptions>
    {
        public void Configure(JwtAuthenticationOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.CookieName))
            {
                options.CookieName = JwtAuthenticationDefaults.CookieName;
            }
        }
    }
}
