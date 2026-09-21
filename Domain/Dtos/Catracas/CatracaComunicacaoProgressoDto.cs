namespace Domain.Dtos.Catracas;

public class CatracaComunicacaoProgressoDto
{
    public int EquipamentoId { get; set; }
    public bool EmAndamento { get; set; }
    public bool Concluido { get; set; }
    public bool Sucesso { get; set; }
    public int Tentativa { get; set; }
    public int MaximoTentativas { get; set; } = 3;
    public string Mensagem { get; set; } = string.Empty;
    public DateTime AtualizadoEm { get; set; }
}
