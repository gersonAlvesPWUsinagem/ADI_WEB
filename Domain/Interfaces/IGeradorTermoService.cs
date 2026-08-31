using Domain.Dtos.Termos;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGeradorTermoService
    {
        Task<ApiDataService<bool>> IndexAsync();
        Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoEmprestimoNotebookAsync(TermoEmprestimoNotebookDto dto);
        Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoDevolucaoNotebookAsync(TermoDevolucaoNotebookDto dto);
        Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoEmprestimoChipAsync(TermoEmprestimoChipDto dto);
        Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoDevolucaoChipAsync(TermoEmprestimoChipDto dto);
        Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoEntregaAcessosAsync(TermoEntregaAcessosDto dto);
        Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoEmprestimoTelefoneAsync(TermoEmprestimoTelefoneDto dto);
        Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarTermoDevolucaoTelefoneAsync(TermoEmprestimoTelefoneDto dto);
    }
}
