using Domain.Dtos.Portaria;
using Domain.Helpers;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPortariaService
    {
        Task<ApiDataService<List<ControlePessoaDto>>> GetColaboradorControladoAsync();
        Task<ApiDataService<List<ColaboradoresControladosDto>>> GetColaboradoresAsync();
        Task<ApiDataService<ColaboradoresControladosDto>> AlternarControleColaboradorAsync(ColaboradoresControladosDto colaborador);
        Task<ApiDataService<List<PorteiroOperadorDto>>> GetPorteiroOperadorAsync();
        Task<ApiDataService<PorteiroOperadorDto>> LoginPorteiroOperadorAsync(int operadorId, string senha);
        Task<ApiDataService<PorteiroOperadorDto>> AdicionarPorteiroOperadorAsync(PorteiroOperadorDto dto);
        Task<ApiDataService<bool>> AtualizarPorteiroOperadorAsync(PorteiroOperadorDto dto);
        Task<ApiDataService<bool>> DeletarPorteiroOperadorAsync(int id);
        Task<ApiDataService<ControlePessoa>> PutRegistrarHorasAsync(ResistrarEntradaSaidaDto data);
        Task<ApiDataService<ControlePessoa>> PatchRegistrarOcorrenciaAsync(PortariaOcorrenciaDto data);
        Task<ApiDataService<ControlePessoa>> PutAlterarHorasAsync(ResistrarEntradaSaidaDto data);
        Task<ApiDataService<bool>> PostGerarListaDeApontamentAsync(DataSession data);
        //Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarRelatorioControlePessoaAsync(string formato, DateTime dataInicial, DateTime dataFinal);
        Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>>
            GerarRelatorioControlePessoaAsync(string formato, DateTime dataInicial, DateTime dataFinal, List<string> matriculas);
        Task<ApiDataService<List<ControlePessoaDto>>> GetColaboradorControladoPorDataAsync(DateTime? dataReferencia = null);
        Task<ApiDataService<List<ControlePessoaDto>>> GetColaboradorControladoPorMesAsync(int ano, int mes);
        Task<ApiDataService<List<int>>> GetMesesColaboradorControladoAsync(int ano);
        Task<ApiDataService<List<PorteiroOperadorDto>>> GetPorteiroOperadorCompAsync();
        Task<ApiDataService<PorteiroOperadorDto>> AlterarSenhaPorteiroOperadorAsync(AlterarSenhaPorteiroOperadorDto dto);
    }
}
