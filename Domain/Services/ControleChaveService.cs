using Domain.Dtos.ControleChaves;
using Domain.Helpers;
using Domain.Interfaces;
namespace Domain.Services;
public class ControleChaveService : BaseHttpService, IControleChaveService
{
    public ControleChaveService(HttpClient httpClient, IDataSessionHelper dataSession, IApiErrorContext errorContext) : base(httpClient, dataSession, errorContext) { }
    public Task<ApiDataService<List<ControleChaveEstoqueDto>>> ListarEstoquesDoUsuarioAsync() => GetAsync<List<ControleChaveEstoqueDto>>("controle-chaves/estoques");
    public Task<ApiDataService<List<ControleChaveEstoqueDto>>> ListarEstoquesAsync() => GetAsync<List<ControleChaveEstoqueDto>>("controle-chaves/estoques/todos");
    public Task<ApiDataService<List<ControleChaveAcessoUsuarioDto>>> ListarUsuariosAcessosAsync(string? pesquisa) => GetAsync<List<ControleChaveAcessoUsuarioDto>>($"controle-chaves/estoques/usuarios?search={Uri.EscapeDataString(pesquisa?.Trim() ?? string.Empty)}");
    public Task<ApiDataService<bool>> DefinirEstoquesDoUsuarioAsync(decimal usuarioId, IReadOnlyCollection<int> estoqueIds) => PutAsync<bool>($"controle-chaves/estoques/usuarios/{usuarioId}", estoqueIds);
    public Task<ApiDataService<List<ControleChaveDto>>> ListarAsync(int estoqueId) => GetAsync<List<ControleChaveDto>>($"controle-chaves?estoqueId={estoqueId}");
    public Task<ApiDataService<List<ControleChaveDto>>> ListarMovimentacoesAsync(int estoqueId, DateTime inicio, DateTime fim, long? matricula = null) =>
        GetAsync<List<ControleChaveDto>>($"controle-chaves/movimentacoes?estoqueId={estoqueId}&inicio={inicio:yyyy-MM-dd}&fim={fim:yyyy-MM-dd}{(matricula.HasValue ? $"&matricula={matricula.Value}" : string.Empty)}");
    public Task<ApiDataService<List<int>>> ListarMesesComMovimentacaoAsync(int estoqueId, int ano) =>
        GetAsync<List<int>>($"controle-chaves/movimentacoes/meses?estoqueId={estoqueId}&ano={ano}");
    public Task<ApiDataService<List<PessoaChaveDto>>> PesquisarPessoasAsync(string? pesquisa) => GetAsync<List<PessoaChaveDto>>($"controle-chaves/pessoas?search={Uri.EscapeDataString(pesquisa?.Trim()??string.Empty)}");
    public Task<ApiDataService<ControleChaveDto>> ObterAsync(int id) => GetAsync<ControleChaveDto>($"controle-chaves/{id}");
    public Task<ApiDataService<ControleChaveDto>> AdicionarAsync(int estoqueId, ControleChaveDto dto) => PostAsync<ControleChaveDto>($"controle-chaves?estoqueId={estoqueId}", dto);
    public Task<ApiDataService<ControleChaveDto>> AtualizarAsync(int id, ControleChaveDto dto) => PutAsync<ControleChaveDto>($"controle-chaves/{id}", dto);
    public Task<ApiDataService<ControleChaveDto>> EmprestarAsync(int id, EmprestarChaveDto dto) => PatchAsync<ControleChaveDto>($"controle-chaves/{id}/emprestar", dto);
    public Task<ApiDataService<ControleChaveDto>> DevolverAsync(int id, DevolverChaveDto dto) => PatchAsync<ControleChaveDto>($"controle-chaves/{id}/devolver", dto);
    public Task<ApiDataService<bool>> ExcluirAsync(int id) => DeleteAsync<bool>($"controle-chaves/{id}");
    public Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarRelatorioAsync(string formato, int estoqueId) => PostForFileAsync("controle-chaves/relatorio", new { Formato=formato, EstoqueId=estoqueId }, $"Controle_Chaves.{(formato=="PDF"?"pdf":"xlsx")}");
    public Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarRelatorioMovimentacoesAsync(string formato, int estoqueId, DateTime inicio, DateTime fim, long? matricula = null, IReadOnlyCollection<int>? ids = null) =>
        PostForFileAsync("controle-chaves/movimentacoes/relatorio", new { Formato=formato, EstoqueId=estoqueId, Inicio=inicio, Fim=fim, Matricula=matricula, MovimentacaoIds=ids }, $"Movimentacoes_Chaves.{(formato=="PDF"?"pdf":"xlsx")}");
}
