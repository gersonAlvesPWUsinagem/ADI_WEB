using Domain.Dtos.Portaria;
using Domain.Helpers;
using Domain.Interfaces;
using Shared.Models;

namespace Domain.Services
{
    public class PortariaService : BaseHttpService, IPortariaService
    {

        public PortariaService(HttpClient httpClient, IDataSessionHelper dataSession)
        : base(httpClient, dataSession)
        {
        }


        public async Task<ApiDataService<List<ControlePessoaDto>>> GetColaboradorControladoAsync()
        {
            var result = await GetAsync<List<ControlePessoaDto>>("portaria/get-colaborador-controlado");
            return result;
        }
        public async Task<ApiDataService<List<ControlePessoaDto>>> GetColaboradorControladoPorDataAsync(DateTime? dataReferencia = null)
        {
            string url = "portaria/get-colaborador-controlado-por-data";
            if (dataReferencia.HasValue)
            {
                // Envia a data no formato ISO padrão (yyyy-MM-dd) para evitar problemas de parsing na API
                url += $"?data={dataReferencia.Value:yyyy-MM-dd}";
            }

            // Altere aqui para deserializar diretamente para a List, e não para ApiDataService<List>
            var result = await GetAsync<List<ControlePessoaDto>>(url);

            return result;
        }
        public Task<ApiDataService<List<ControlePessoaDto>>> GetColaboradorControladoPorMesAsync(int ano, int mes) =>
            GetAsync<List<ControlePessoaDto>>($"portaria/get-colaborador-controlado-por-mes?ano={ano}&mes={mes}");

        public Task<ApiDataService<List<int>>> GetMesesColaboradorControladoAsync(int ano) =>
            GetAsync<List<int>>($"portaria/get-meses-colaborador-controlado?ano={ano}");
        public Task<ApiDataService<List<ColaboradoresControladosDto>>> GetColaboradoresAsync(string? search = null) =>
            GetAsync<List<ColaboradoresControladosDto>>(
                string.IsNullOrWhiteSpace(search)
                    ? "portaria/get-colaboradores"
                    : $"portaria/get-colaboradores?search={Uri.EscapeDataString(search.Trim())}");

        public Task<ApiDataService<ColaboradoresControladosDto>> AlternarControleColaboradorAsync(ColaboradoresControladosDto colaborador) =>
            PatchAsync<ColaboradoresControladosDto>("portaria/patch-controle-colaborador", new
            {
                Id = colaborador.ControladoId,
                PessoaId = 0,
                EmpFil = colaborador.EMPFIL,
                Matricula = colaborador.MATRICULA,
                DataCadastro = DateTime.Now
            });
        public async Task<ApiDataService<List<PorteiroOperadorDto>>> GetPorteiroOperadorAsync()
        {
            var result = await GetAsync<List<PorteiroOperadorDto>>("portaria/get-porteiro-operador");
            return result;
        }
        public async Task<ApiDataService<List<PorteiroOperadorDto>>> GetPorteiroOperadorCompAsync()
        {
            var result = await GetAsync<List<PorteiroOperadorDto>>("portaria/get-porteiro-operador-componente");
            return result;
        }
        public Task<ApiDataService<PorteiroOperadorDto>> LoginPorteiroOperadorAsync(int operadorId, string senha) =>
            PostAsync<PorteiroOperadorDto>("portaria/login-porteiro-operador", new { OperadorId = operadorId, Senha = senha });

        public Task<ApiDataService<PorteiroOperadorDto>> AlterarSenhaPorteiroOperadorAsync(AlterarSenhaPorteiroOperadorDto dto) =>
            PutAsync<PorteiroOperadorDto>("portaria/alterar-senha-porteiro-operador", dto);

        public Task<ApiDataService<PorteiroOperadorDto>> AdicionarPorteiroOperadorAsync(PorteiroOperadorDto dto) =>
            PostAsync<PorteiroOperadorDto>("portaria/post-porteiro-operador", dto);

        public Task<ApiDataService<bool>> AtualizarPorteiroOperadorAsync(PorteiroOperadorDto dto) =>
            PutAsync<bool>($"portaria/put-porteiro-operador/{dto.PoreteiroID}", dto);

        public Task<ApiDataService<bool>> DeletarPorteiroOperadorAsync(int id) =>
            DeleteAsync<bool>($"portaria/delete-porteiro-operador/{id}");
        public async Task<ApiDataService<ControlePessoa>> PutRegistrarHorasAsync(ResistrarEntradaSaidaDto data)
        {
            ApiDataService<ControlePessoa> result = new();

            result = await PutAsync<ControlePessoa>("portaria/put-registrar-entrada-saida", data);

            return result;
        }
        public async Task<ApiDataService<ControlePessoa>> PutAlterarHorasAsync(ResistrarEntradaSaidaDto data)
        {
            ApiDataService<ControlePessoa> result = new();

            result = await PutAsync<ControlePessoa>("portaria/put-alterar-entrada-saida", data);

            return result;
        }
        public async Task<ApiDataService<bool>> PostGerarListaDeApontamentAsync(DataSession data)
        {
            ApiDataService<bool> result = new();

            result = await PostAsync<bool>("portaria/post-gerar-lista-apontados", data);
            return result;
        }
        public async Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>>
            GerarRelatorioControlePessoaAsync(string formato, DateTime dataInicial, DateTime dataFinal, List<string> matriculas)
        {
            var result = await PostForFileAsync("portaria/exportar-relatorio-controle-pessoa", new { Formato = formato, DataInicial = dataInicial, DataFinal = dataFinal, Matriculas = matriculas }, $"Relatorio_Controle_Pessoa.{formato.ToLowerInvariant()}");

            return result;
        }
        public async Task<ApiDataService<ControlePessoa>> PatchRegistrarOcorrenciaAsync(PortariaOcorrenciaDto data)
        {
            var result = await PatchAsync<ControlePessoa>("Portaria/PatchOcorrencia", data);
            return result;
        }
        public async Task<ApiDataService<IEnumerable<ColaboradoresControladosDto>>> GetColaboradorAsync()
        {
            var result = await GetAsync<IEnumerable<ColaboradoresControladosDto>>("/portaria");
            return result;
        }
    }
}
