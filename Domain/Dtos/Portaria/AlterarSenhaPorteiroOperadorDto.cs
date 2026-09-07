namespace Domain.Dtos.Portaria;

public sealed class AlterarSenhaPorteiroOperadorDto
{
    public int OperadorId { get; set; }
    public string SenhaAtual { get; set; } = string.Empty;
    public string NovaSenha { get; set; } = string.Empty;
    public string ConfirmacaoNovaSenha { get; set; } = string.Empty;
}
