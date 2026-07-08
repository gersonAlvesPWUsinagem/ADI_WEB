using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace ADI_WEB.Security;

/// <summary>
/// Centralizes authentication cookie manipulation.
/// </summary>
public sealed class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthCookieJsInterop _authCookieJsInterop;
    private readonly JwtAuthenticationOptions _options;
    private readonly ILogger<CookieService> _logger;
    private readonly JwtTokenNormalizer _tokenNormalizer;
    private string? _token;

    /// <summary>
    /// Initializes a new instance of the <see cref="CookieService"/> class.
    /// </summary>
    public CookieService(
        IHttpContextAccessor httpContextAccessor,
        AuthCookieJsInterop authCookieJsInterop,
        IOptions<JwtAuthenticationOptions> options,
        ILogger<CookieService> logger,
        JwtTokenNormalizer tokenNormalizer)
    {
        _httpContextAccessor = httpContextAccessor;
        _authCookieJsInterop = authCookieJsInterop;
        _options = options.Value;
        _logger = logger;
        _tokenNormalizer = tokenNormalizer;
    }

    /// <inheritdoc />
    public async Task SetAuthTokenAsync(string token)
    {
        _token = _tokenNormalizer.Normalize(token);

        if (_options.HttpOnly)
        {
            _logger.LogWarning("HttpOnly=true foi configurado, mas cookies criados por JavaScript nao podem receber HttpOnly.");
        }

        await _authCookieJsInterop.SetCookieAsync(
            _options.CookieName,
            _token,
            GetMaxAgeSeconds(_token),
            _options.SameSite.ToString(),
            ShouldWriteSecureCookie(),
            _options.CookiePath);
    }

    /// <inheritdoc />
    public async Task<string?> GetAuthTokenAsync()
    {
        if (!string.IsNullOrWhiteSpace(_token))
        {
            return _token;
        }

        if (_httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(_options.CookieName, out var requestToken) == true)
        {
            _token = _tokenNormalizer.Normalize(requestToken);
            return _token;
        }

        try
        {
            _token = _tokenNormalizer.Normalize(await _authCookieJsInterop.GetCookieAsync(_options.CookieName) ?? string.Empty);
            return _token;
        }
        catch (JSException ex)
        {
            _logger.LogDebug(ex, "Cookie nao pode ser lido por JS neste momento.");
            return null;
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogDebug(ex, "Cookie nao pode ser lido por JS antes do circuito interativo.");
            return null;
        }
    }

    /// <inheritdoc />
    public async Task RemoveAuthTokenAsync()
    {
        _token = null;
        await _authCookieJsInterop.RemoveCookieAsync(
            _options.CookieName,
            _options.SameSite.ToString(),
            ShouldWriteSecureCookie(),
            _options.CookiePath);
    }

    private bool ShouldWriteSecureCookie()
    {
        return _options.SecurePolicy switch
        {
            CookieSecurePolicy.Always => true,
            CookieSecurePolicy.None => false,
            _ => _httpContextAccessor.HttpContext?.Request.IsHttps == true
        };
    }

    private static int GetMaxAgeSeconds(string token)
    {
        try
        {
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var exp = jwt.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;

            if (long.TryParse(exp, out var unixSeconds))
            {
                var seconds = (int)Math.Max(0, DateTimeOffset.FromUnixTimeSeconds(unixSeconds).Subtract(DateTimeOffset.UtcNow).TotalSeconds);
                return seconds;
            }
        }
        catch
        {
            return 36000;
        }

        return 36000;
    }
}
