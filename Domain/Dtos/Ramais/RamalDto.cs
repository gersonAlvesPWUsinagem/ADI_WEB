using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Ramais;

public class RamalDto
{
    public int Id { get; set; }
    public int RamalId { get; set; }
    public long EmpFil { get; set; }
    public long Matricula { get; set; }
    public long PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Departamento { get; set; }
    [Required(ErrorMessage = "O ramal é obrigatório.")]
    [RegularExpression(@"^\d{1,4}$", ErrorMessage = "Informe um ramal numérico de até 4 dígitos.")]
    public string Ramal { get; set; } = string.Empty;
    [StringLength(20)] public string? Ddr { get; set; }
    [StringLength(25)] public string? Celular { get; set; }
    [StringLength(100)] public string? Setor { get; set; }
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [StringLength(160)] public string? Email { get; set; }
}
