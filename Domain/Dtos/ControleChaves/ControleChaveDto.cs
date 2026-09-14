using System.ComponentModel.DataAnnotations;
namespace Domain.Dtos.ControleChaves;
public class ControleChaveDto
{
    public int Id { get; set; }
    [Required, StringLength(50)] public string Identificacao { get; set; } = string.Empty;
    [Required, StringLength(150)] public string Descricao { get; set; } = string.Empty;
    [StringLength(150)] public string? Localizacao { get; set; }
    [StringLength(150)] public string? Responsavel { get; set; }
    public bool Disponivel { get; set; } = true;
    public long? EmpFilEmprestimo { get; set; }
    public long? MatriculaEmprestimo { get; set; }
    public long? PessoaIdEmprestimo { get; set; }
    public string? NomePessoaEmprestimo { get; set; }
    public DateTime? DataEmprestimo { get; set; }
    public int? PorteiroEmprestimoId { get; set; }
    public string? PorteiroEmprestimoNome { get; set; }
    public string? ObservacaoEmprestimo { get; set; }
    public long? EmpFilDevolucao { get; set; }
    public long? MatriculaDevolucao { get; set; }
    public long? PessoaIdDevolucao { get; set; }
    public string? NomePessoaDevolucao { get; set; }
    public DateTime? DataDevolucao { get; set; }
    public int? PorteiroDevolucaoId { get; set; }
    public string? PorteiroDevolucaoNome { get; set; }
    [StringLength(500)] public string? Observacao { get; set; }
    public DateTime CriadoEm { get; set; }
    public int? PorteiroCadastroId { get; set; }
    public string? PorteiroCadastroNome { get; set; }
    public int? PorteiroOperacaoId { get; set; }
    public DateTime AtualizadoEm { get; set; }
}
public class PessoaChaveDto { public long EmpFil { get; set; } public long Matricula { get; set; } public long PessoaId { get; set; } public string Nome { get; set; } = string.Empty; public string? Departamento { get; set; } }
public class EmprestarChaveDto
{
    public long EmpFil { get; set; }
    public long Matricula { get; set; }
    public long PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int PorteiroId { get; set; }
    public string? Observacao { get; set; }
}
public class DevolverChaveDto
{
    public long EmpFil { get; set; }
    public long Matricula { get; set; }
    public long PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int PorteiroId { get; set; }
}
