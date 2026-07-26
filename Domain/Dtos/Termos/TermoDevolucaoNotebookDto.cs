using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Termos
{
    public class TermoDevolucaoNotebookDto
    {
        public string? ColaboradorDocumento { get; set; }
        [Required]
        public string? NotebookId { get; set; }
        public bool IsForPdfGeneration { get; set; }
    }
}
