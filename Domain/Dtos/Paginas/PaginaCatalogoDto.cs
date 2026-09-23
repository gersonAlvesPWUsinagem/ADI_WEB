namespace Domain.Dtos.Paginas;

public class PaginaCatalogoDto
{
    public int? Id { get; set; }
    public string Chave { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Rota { get; set; } = string.Empty;
    public string? Icone { get; set; }
    public string Modulo { get; set; } = string.Empty;
    public int Permissao { get; set; }
    public bool Ativo { get; set; } = true;
    public string? RotaBanco { get; set; }
    public string? IconeBanco { get; set; }
    public string? ChaveBanco { get; set; }
    public int? IdCorreto { get; set; }
    public int? IdUrlPages { get; set; }
    public bool SomenteBanco { get; set; }
    public bool NaoCadastrada => !SomenteBanco && !Id.HasValue && !IdCorreto.HasValue;
    public bool IdAusente => !SomenteBanco && !Id.HasValue && IdCorreto.HasValue;
    public string Status { get; set; } = "Default";
    public bool Integrada => Id.HasValue;
    public bool RotaAlterada => RotaBanco is not null && !string.Equals(Rota, RotaBanco, StringComparison.OrdinalIgnoreCase);
    public bool IconeAlterado => IconeBanco is not null && !string.Equals(Icone, IconeBanco, StringComparison.Ordinal);
    public bool IdDivergente => Id.HasValue && (ChaveBanco is null || (IdCorreto.HasValue && Id != IdCorreto));
}
