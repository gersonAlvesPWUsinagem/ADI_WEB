using Domain.Dtos.ControleChaves;
using Domain.Helpers;
using Domain.Interfaces;
namespace Domain.Services;
public class ControleChaveService : BaseHttpService, IControleChaveService
{
    public ControleChaveService(HttpClient httpClient, IDataSessionHelper dataSession, IApiErrorContext errorContext) : base(httpClient, dataSession, errorContext) { }
    public Task<ApiDataService<List<ControleChaveDto>>> ListarAsync() => GetAsync<List<ControleChaveDto>>("controle-chaves");
    public Task<ApiDataService<List<ControleChaveDto>>> ListarMovimentacoesAsync(DateTime inicio, DateTime fim, long? matricula = null) =>
        GetAsync<List<ControleChaveDto>>($"controle-chaves/movimentacoes?inicio={inicio:yyyy-MM-dd}&fim={fim:yyyy-MM-dd}{(matricula.HasValue ? $"&matricula={matricula.Value}" : string.Empty)}");
    public Task<ApiDataService<List<int>>> ListarMesesComMovimentacaoAsync(int ano) =>
        GetAsync<List<int>>($"controle-chaves/movimentacoes/meses?ano={ano}");
    public Task<ApiDataService<List<PessoaChaveDto>>> PesquisarPessoasAsync(string? pesquisa) => GetAsync<List<PessoaChaveDto>>($"controle-chaves/pessoas?search={Uri.EscapeDataString(pesquisa?.Trim()??string.Empty)}");
    public Task<ApiDataService<ControleChaveDto>> ObterAsync(int id) => GetAsync<ControleChaveDto>($"controle-chaves/{id}");
    public Task<ApiDataService<ControleChaveDto>> AdicionarAsync(ControleChaveDto dto) => PostAsync<ControleChaveDto>("controle-chaves", dto);
    public Task<ApiDataService<ControleChaveDto>> AtualizarAsync(int id, ControleChaveDto dto) => PutAsync<ControleChaveDto>($"controle-chaves/{id}", dto);
    public Task<ApiDataService<ControleChaveDto>> EmprestarAsync(int id, EmprestarChaveDto dto) => PatchAsync<ControleChaveDto>($"controle-chaves/{id}/emprestar", dto);
    public Task<ApiDataService<ControleChaveDto>> DevolverAsync(int id, DevolverChaveDto dto) => PatchAsync<ControleChaveDto>($"controle-chaves/{id}/devolver", dto);
    public Task<ApiDataService<bool>> ExcluirAsync(int id) => DeleteAsync<bool>($"controle-chaves/{id}");
    public Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarRelatorioAsync(string formato) => PostForFileAsync("controle-chaves/relatorio", new { Formato=formato }, $"Controle_Chaves.{(formato=="PDF"?"pdf":"xlsx")}");
    public Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarRelatorioMovimentacoesAsync(string formato, DateTime inicio, DateTime fim, long? matricula = null, IReadOnlyCollection<int>? ids = null) =>
        PostForFileAsync("controle-chaves/movimentacoes/relatorio", new { Formato=formato, Inicio=inicio, Fim=fim, Matricula=matricula, MovimentacaoIds=ids }, $"Movimentacoes_Chaves.{(formato=="PDF"?"pdf":"xlsx")}");
}
