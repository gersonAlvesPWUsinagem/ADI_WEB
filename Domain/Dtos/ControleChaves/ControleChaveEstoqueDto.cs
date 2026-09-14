namespace Domain.Dtos.ControleChaves;

public class ControleChaveEstoqueDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}

public class ControleChaveAcessoUsuarioDto
{
    public decimal UsuarioId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public decimal Matricula { get; set; }
    public List<int> EstoqueIds { get; set; } = [];
}
