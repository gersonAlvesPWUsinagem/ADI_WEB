using Domain.Helpers;
using Shared.Enums;

namespace ADI_WEB.Security;

/// <summary>
/// Formats the Environment claim using the EnvironmentEnum display metadata.
/// </summary>
public static class EnvironmentClaimFormatter
{
    /// <summary>
    /// Converts a claim value to its display name and optional description.
    /// </summary>
    public static string? Format(string? value, bool includeDescription = false)
    {
        if (!TryParse(value, out var environment))
        {
            return value;
        }

        var displayName = environment.GetDisplayName();
        if (!includeDescription)
        {
            return displayName;
        }

        var description = environment.GetDisplayDescription();
        return string.IsNullOrWhiteSpace(description)
            ? displayName
            : $"{displayName} {description}";
    }

    /// <summary>
    /// Converts a claim value to EnvironmentEnum.
    /// </summary>
    public static EnvironmentEnum ParseOrDefault(string? value)
    {
        return TryParse(value, out var environment)
            ? environment
            : EnvironmentEnum.Development;
    }

    private static bool TryParse(string? value, out EnvironmentEnum environment)
    {
        environment = EnvironmentEnum.Development;

        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        if (int.TryParse(value, out var numericValue) && Enum.IsDefined(typeof(EnvironmentEnum), numericValue))
        {
            environment = (EnvironmentEnum)numericValue;
            return true;
        }

        if (Enum.TryParse(value, ignoreCase: true, out environment))
        {
            return true;
        }

        foreach (EnvironmentEnum item in Enum.GetValues(typeof(EnvironmentEnum)))
        {
            if (string.Equals(item.GetDisplayName(), value, StringComparison.OrdinalIgnoreCase))
            {
                environment = item;
                return true;
            }
        }

        return false;
    }
}
