using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Termos
{
    public class TermoEmprestimoTelefoneDto
    {
        public TermoEmprestimoTelefoneDto()
        {
            TypeEquipamento = "USADO";
        }
        public string? ColaboradorNome { get; set; }

        public string? ColaboradorDocumento { get; set; }

        [Required]
        public string? TelefoneId { get; set; }
        public string? TypeEquipamento { get; set; }
        public bool IsForPdfGeneration { get; set; }
    }
}
