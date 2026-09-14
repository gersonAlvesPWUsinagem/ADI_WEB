using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ApiErrors;

public class RegistrarApiErrorDto
{
    [Required, StringLength(300)] public string Tela { get; set; } = string.Empty;
    [StringLength(500)] public string Endpoint { get; set; } = string.Empty;
    [Required, StringLength(4000)] public string MensagemErro { get; set; } = string.Empty;
    [StringLength(100)] public string? IdentificadorUsuario { get; set; }
}

public class ApiErrorLogDto
{
    public long Id { get; set; }
    public DateTime DataHora { get; set; }
    public string Tela { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public string UsuarioNome { get; set; } = string.Empty;
    public string UsuarioLogin { get; set; } = string.Empty;
    public int MatriculaUsuario { get; set; }
    public long? MatriculaPessoa { get; set; }
    public string MensagemErro { get; set; } = string.Empty;
}
