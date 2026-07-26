using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Termos
{
    public class TermoEmprestimoNotebookDto
    {
        public TermoEmprestimoNotebookDto()
        {
            TypeEquipamento  = "USADO";
        }
        public string? ColaboradorNome { get; set; } 

        public string? ColaboradorDocumento { get; set; } 

        [Required]
        public string? NotebookId { get; set; }
        public string? TypeEquipamento { get; set; }
        public bool IsForPdfGeneration { get; set; } 
    }
}
