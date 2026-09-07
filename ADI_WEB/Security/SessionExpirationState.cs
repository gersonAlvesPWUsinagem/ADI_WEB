namespace ADI_WEB.Security;

/// <summary>
/// Centraliza a notificação de uma resposta da API que indica sessão expirada.
/// </summary>
public sealed class SessionExpirationState
{
    private int _notificado;

    public event Func<string, Task>? SessionExpired;

    public async Task NotifyAsync(string returnUrl)
    {
        if (Interlocked.Exchange(ref _notificado, 1) != 0)
        {
            return;
        }

        if (SessionExpired is not null)
        {
            await SessionExpired.Invoke(returnUrl);
        }
    }
}
