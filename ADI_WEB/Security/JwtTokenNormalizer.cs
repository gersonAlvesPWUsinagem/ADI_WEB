namespace ADI_WEB.Security;

using System.Text.Json;

/// <summary>
/// Normalizes token values returned by the API into a raw JWT string.
/// </summary>
public sealed class JwtTokenNormalizer
{
    /// <summary>
    /// Extracts the raw JWT from supported token formats.
    /// </summary>
    public string Normalize(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return string.Empty;
        }

        var normalizedToken = token.Trim().Trim('"').Trim('\'').Trim();

        if (normalizedToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            normalizedToken = normalizedToken["Bearer ".Length..].Trim();
        }

        if (normalizedToken.StartsWith("{", StringComparison.Ordinal))
        {
            try
            {
                using var document = JsonDocument.Parse(normalizedToken);
                var root = document.RootElement;
                if (root.TryGetProperty("token", out var tokenProperty) || root.TryGetProperty("Token", out tokenProperty))
                {
                    normalizedToken = tokenProperty.GetString() ?? string.Empty;
                }
            }
            catch (JsonException)
            {
                var separatorIndex = normalizedToken.IndexOf('=');
                if (separatorIndex >= 0)
                {
                    normalizedToken = normalizedToken[(separatorIndex + 1)..]
                        .Replace("}", string.Empty)
                        .Trim()
                        .Trim('"')
                        .Trim('\'');
                }
            }
        }

        return normalizedToken;
    }
}
