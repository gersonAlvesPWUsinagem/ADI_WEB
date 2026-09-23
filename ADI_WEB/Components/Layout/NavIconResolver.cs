using System.Reflection;
using MudBlazor;

namespace ADI_WEB.Components.Layout;

public static class NavIconResolver
{
    public static string Resolve(string? chave)
    {
        if (string.IsNullOrWhiteSpace(chave)) return Icons.Material.Filled.Link;
        // Ícones Material do MudBlazor são armazenados como markup SVG (<path ...>).
        // Devem ser devolvidos diretamente, sem tentar tratá-los como nome de chave.
        if (chave.StartsWith("<", StringComparison.Ordinal) || chave.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase)) return chave;
        if (chave.Contains('.')) return chave;
        var property = typeof(Icons.Material.Filled).GetProperty(chave, BindingFlags.Public | BindingFlags.Static);
        return property?.GetValue(null)?.ToString() ?? Icons.Material.Filled.Link;
    }
}
