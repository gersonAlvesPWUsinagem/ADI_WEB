namespace ADI_WEB.Security;

public static class ThemePreference
{
    public const string CookieName = "adi_dark_mode";

    public static bool GetInitialDarkMode(IHttpContextAccessor httpContextAccessor)
    {
        var value = httpContextAccessor.HttpContext?.Request.Cookies[CookieName];
        return bool.TryParse(value, out var isDarkMode) && isDarkMode;
    }
}
