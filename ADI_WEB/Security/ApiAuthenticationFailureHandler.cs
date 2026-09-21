using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace ADI_WEB.Security;

/// <summary>
/// Detecta respostas de sessão expirada de qualquer chamada à API.
/// </summary>
public sealed class ApiAuthenticationFailureHandler : DelegatingHandler
{
    private readonly NavigationManager _navigation;
    private readonly SessionExpirationState _sessionExpirationState;

    public ApiAuthenticationFailureHandler(
        NavigationManager navigation,
        SessionExpirationState sessionExpirationState)
    {
        _navigation = navigation;
        _sessionExpirationState = sessionExpirationState;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        var paginaAtual = _navigation.ToBaseRelativePath(_navigation.Uri).Split('?', '#')[0].Trim('/');
        var estaNoLogin = paginaAtual.StartsWith("user/login", StringComparison.OrdinalIgnoreCase);
        if (!estaNoLogin && !IsOperatorLoginRequest(request) && await IsExpiredSessionAsync(response, cancellationToken))
        {
            var returnUrl = _navigation.ToBaseRelativePath(_navigation.Uri);
            await _sessionExpirationState.NotifyAsync(returnUrl);
        }

        return response;
    }

    private static bool IsOperatorLoginRequest(HttpRequestMessage request)
    {
        return request.RequestUri?.AbsolutePath.EndsWith(
            "/recursos-humanos/portaria/login-porteiro-operador",
            StringComparison.OrdinalIgnoreCase) == true;
    }

    private static async Task<bool> IsExpiredSessionAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            return true;
        }

        if (response.StatusCode != HttpStatusCode.BadRequest || response.Content is null)
        {
            return false;
        }

        try
        {
            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            return root.TryGetProperty("statusCode", out var statusCode) && statusCode.GetInt32() == 606;
        }
        catch (JsonException)
        {
            return false;
        }
    }
}
