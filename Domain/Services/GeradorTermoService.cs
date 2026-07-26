using Domain.Dtos.Termos;
using Domain.Helpers;
using Domain.Interfaces;

namespace Domain.Services
{
    public class GeradorTermoService : BaseHttpService, IGeradorTermoService
    {
        public GeradorTermoService(HttpClient httpClient, IDataSessionHelper dataSession)
            : base(httpClient, dataSession)
        {
        }

        public async Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoEmprestimoNotebookAsync(TermoEmprestimoNotebookDto dto)
        {
            return await PostForFileAsync("Termos/termo-emprestimo-notebook", dto, "termo-emprestimo-notebook.pdf");
        }

        public async Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoDevolucaoNotebookAsync(TermoDevolucaoNotebookDto dto)
        {
            return await PostForFileAsync("Termos/termo-devolucao-notebook", dto, "termo-devolucao-notebook.pdf");
        }

        public async Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoEmprestimoChipAsync(TermoEmprestimoChipDto dto)
        {
            return await PostForFileAsync("Termos/termo-emprestimo-chip", dto, "termo-emprestimo-chip.pdf");
        }

        public async Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoDevolucaoChipAsync(TermoEmprestimoChipDto dto)
        {
            return await PostForFileAsync("Termos/termo-devolucao-chip", dto, "termo-devolucao-chip.pdf");
        }

        public async Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoEntregaAcessosAsync(TermoEntregaAcessosDto dto)
        {
            return await PostForFileAsync("Termos/termo-entrega-acessos", dto, "termo-entrega-acessos.pdf");
        }

        public async Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoEmprestimoTelefoneAsync(TermoEmprestimoTelefoneDto dto)
        {
            return await PostForFileAsync("Termos/termo-emprestimo-telefone", dto, "termo-emprestimo-celular.pdf");
        }

        public async Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoDevolucaoTelefoneAsync(TermoEmprestimoTelefoneDto dto)
        {
            return await PostForFileAsync("Termos/termo-devolucao-telefone", dto, "termo-devolucao-celular.pdf");
        }
    }
}