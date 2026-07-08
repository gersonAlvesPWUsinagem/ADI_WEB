using Microsoft.JSInterop;

namespace ADI_WEB.Security;

/// <summary>
/// Provides centralized JavaScript interop for authentication cookie operations.
/// </summary>
public sealed class AuthCookieJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthCookieJsInterop"/> class.
    /// </summary>
    public AuthCookieJsInterop(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>("import", "./security/authCookie.js").AsTask());
    }

    /// <summary>
    /// Creates or updates an authentication cookie.
    /// </summary>
    public async Task SetCookieAsync(string name, string value, int maxAgeSeconds, string sameSite, bool secure, string path)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("setCookie", name, value, maxAgeSeconds, sameSite, secure, path);
    }

    /// <summary>
    /// Reads an authentication cookie.
    /// </summary>
    public async Task<string?> GetCookieAsync(string name)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<string?>("getCookie", name);
    }

    /// <summary>
    /// Removes an authentication cookie.
    /// </summary>
    public async Task RemoveCookieAsync(string name, string sameSite, bool secure, string path)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("removeCookie", name, sameSite, secure, path);
    }

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
