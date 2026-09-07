namespace Domain.Dtos.Favoritos;
public class FavoritoPaginaDto
{
    public int Id { get; set; }
    public string Rota { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string? Icone { get; set; }
}
