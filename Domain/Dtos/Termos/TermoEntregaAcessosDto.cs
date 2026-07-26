namespace Domain.Dtos.Termos
{
    public class TermoEntregaAcessosDto
    {
        public string? ColaboradorNome { get; set; }
        public string? Departamento { get; set; }
        public string? DataCadastro { get; set; } // Opcional, se vazio preenche com a data atual ou placeholders
        public string? UsuarioWindows { get; set; }
        public string? SenhaWindows { get; set; }
        public string? EmailCorporativo { get; set; }
        public string? SenhaEmail { get; set; }
        public string? SistemaOmegaUsuario { get; set; }
        public string? SistemaOmegaSenha { get; set; }
        public string? SistemaAdiUsuario { get; set; }
        public string? SistemaAdiSenha { get; set; }
        public bool IsForPdfGeneration { get; set; }
    }
}
