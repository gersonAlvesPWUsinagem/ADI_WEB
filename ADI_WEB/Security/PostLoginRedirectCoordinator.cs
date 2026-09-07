using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace ADI_WEB.Security;

/// <summary>
/// Preserva o destino solicitado durante o login, mesmo quando páginas legadas
/// ainda direcionam explicitamente para /main após autenticar.
/// </summary>
public sealed class PostLoginRedirectCoordinator : IDisposable
{
    private readonly NavigationManager _navigation;
    private IDisposable? _registration;
    private string? _destination;

    public PostLoginRedirectCoordinator(NavigationManager navigation)
    {
        _navigation = navigation;
    }

    public void PrepararDestinoDaPaginaDeLogin()
    {
        // No prerender o RemoteNavigationManager ainda nao foi inicializado.
        // O registro so e necessario apos o login, quando a navegacao ja esta interativa.
        _registration ??= _navigation.RegisterLocationChangingHandler(OnLocationChangingAsync);

        var uri = _navigation.ToAbsoluteUri(_navigation.Uri);
        var retorno = uri.Query.TrimStart('?')
            .Split('&', StringSplitOptions.RemoveEmptyEntries)
            .Select(item => item.Split('=', 2))
            .FirstOrDefault(item => item.Length == 2 && string.Equals(item[0], "returnUrl", StringComparison.OrdinalIgnoreCase));

        if (retorno is null)
        {
            _destination = null;
            return;
        }

        var destino = Uri.UnescapeDataString(retorno[1]);
        _destination = EhDestinoLocalSeguro(destino) ? destino : null;
    }

    private async ValueTask OnLocationChangingAsync(LocationChangingContext context)
    {
        if (string.IsNullOrWhiteSpace(_destination))
        {
            return;
        }

        var rotaDestino = _navigation.ToBaseRelativePath(context.TargetLocation).Trim('/');
        if (!string.Equals(rotaDestino, "main", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        var destino = _destination;
        _destination = null;
        context.PreventNavigation();

        await Task.Yield();
        _navigation.NavigateTo(destino, forceLoad: true);
    }

    private static bool EhDestinoLocalSeguro(string destino) =>
        destino.StartsWith('/') &&
        !destino.StartsWith("//") &&
        !destino.StartsWith("/user/login", StringComparison.OrdinalIgnoreCase);

    public void Dispose() => _registration?.Dispose();
}
