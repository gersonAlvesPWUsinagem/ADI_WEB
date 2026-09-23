namespace ADI_WEB.Components.Layout;

public sealed class NavMenuItem
{
    private NavMenuItem(string icone, string? rota, IReadOnlyDictionary<string, NavMenuItem>? filhos, int? id, string? chave, string modulo, int permissao, bool catalogo, string? nome)
    {
        Icone = icone;
        Rota = rota;
        Filhos = filhos;
        Id = id;
        Chave = chave ?? rota?.Trim('/');
        Modulo = modulo;
        Permissao = permissao;
        ParticipaCatalogo = catalogo;
        Nome = nome;
    }

    public string Icone { get; private set; }
    public string? Rota { get; private set; }
    public int? Id { get; }
    public string? Chave { get; }
    public string Modulo { get; }
    public int Permissao { get; }
    public bool ParticipaCatalogo { get; }
    public string? Nome { get; }
    public IReadOnlyDictionary<string, NavMenuItem>? Filhos { get; }
    public bool EhGrupo => Filhos is not null;
    public bool EhImagem => Icone.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase);

    public static NavMenuItem Pagina(string icone, string rota, int? id = null, string? chave = null, string modulo = "", int permissao = 0, bool catalogo = true, string? Nome = null) => new(icone, rota, null, id, chave, modulo, permissao, catalogo, Nome);

    public static NavMenuItem Grupo(string icone, IReadOnlyDictionary<string, NavMenuItem> filhos) => new(icone, null, filhos, null, null, "", 0, true, null);

    public void AplicarCatalogo(string? rota, string? icone)
    {
        if (!string.IsNullOrWhiteSpace(rota)) Rota = rota;
        if (!string.IsNullOrWhiteSpace(icone)) Icone = icone;
    }
}
