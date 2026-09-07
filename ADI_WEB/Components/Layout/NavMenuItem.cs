namespace ADI_WEB.Components.Layout;

public sealed class NavMenuItem
{
    private NavMenuItem(string icone, string? rota, IReadOnlyDictionary<string, NavMenuItem>? filhos)
    {
        Icone = icone;
        Rota = rota;
        Filhos = filhos;
    }

    public string Icone { get; }
    public string? Rota { get; }
    public IReadOnlyDictionary<string, NavMenuItem>? Filhos { get; }
    public bool EhGrupo => Filhos is not null;

    public static NavMenuItem Pagina(string icone, string rota) => new(icone, rota, null);

    public static NavMenuItem Grupo(string icone, IReadOnlyDictionary<string, NavMenuItem> filhos) => new(icone, null, filhos);
}
