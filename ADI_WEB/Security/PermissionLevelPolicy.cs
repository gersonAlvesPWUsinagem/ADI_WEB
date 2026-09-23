using System.Globalization;

namespace ADI_WEB.Security;

/// <summary>
/// Regra única para a hierarquia de níveis de acesso da interface.
/// Um nível maior inclui todos os níveis menores.
/// </summary>
public static class PermissionLevelPolicy
{
    public const int MinimumLevel = 0;
    public const int MaximumLevel = 6;

    public static bool Allows(int userLevel, int requiredLevel) =>
        userLevel is >= MinimumLevel and <= MaximumLevel &&
        requiredLevel is >= MinimumLevel and <= MaximumLevel &&
        userLevel >= requiredLevel;

    public static bool Allows(IEnumerable<int> userLevels, int requiredLevel) =>
        userLevels.Any(userLevel => Allows(userLevel, requiredLevel));

    public static bool Allows(string? userLevel, int requiredLevel)
    {
        if (int.TryParse(userLevel, NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericLevel))
            return Allows(numericLevel, requiredLevel);

        var normalized = userLevel?.Trim().ToUpperInvariant();
        var mappedLevel = normalized switch
        {
            "ADMINISTRADOR" or "ADMIN" => 6,
            "SUPERVISÃO" or "SUPERVISAO" => 5,
            "EXCLUSÃO" or "EXCLUSAO" => 4,
            "ALTERAÇÃO" or "ALTERACAO" => 3,
            "GRAVAÇÃO" or "GRAVACAO" => 2,
            "LEITURA" or "OPERACIONAL" => 1,
            _ => -1
        };

        return Allows(mappedLevel, requiredLevel);
    }
}
