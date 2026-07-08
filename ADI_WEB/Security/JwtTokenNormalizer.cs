namespace ADI_WEB.Security;

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

        if (normalizedToken.StartsWith("{", StringComparison.Ordinal) && normalizedToken.Contains('='))
        {
            normalizedToken = normalizedToken[(normalizedToken.IndexOf('=') + 1)..];
            normalizedToken = normalizedToken.Replace("}", string.Empty).Trim().Trim('"').Trim('\'').Trim();
        }

        return normalizedToken;
    }
}
