using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Dtos.Portaria;

public sealed class RelatorioControlePessoaRequestDto
{
    public string Formato { get; set; } = "PDF";
    public DateTime DataInicial { get; set; }
    public DateTime DataFinal { get; set; }
    public List<string>? Matriculas { get; set; }

    [NotMapped]
    public bool FiltrarApenasSelecionados { get; set; }
}


