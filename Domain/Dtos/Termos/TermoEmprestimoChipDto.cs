namespace Domain.Dtos.Termos
{
    public class TermoEmprestimoChipDto
    {
        public string? ColaboradorNome { get; set; }
        public string? Documento { get; set; } // RG ou CPF
        public string? ModeloChip { get; set; } // Ex: "Chip Claro Móvel Plano:"
        public string? NumeroTelefone { get; set; } // Ex: "(11) 97322-0669"
        public string? SimCard { get; set; } // Ex: "89550537100001674082"
        public string? DataCadastro { get; set; }
        public bool IsForPdfGeneration { get; set; }
    }
}
