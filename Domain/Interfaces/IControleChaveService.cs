using Domain.Dtos.ControleChaves;
using Domain.Helpers;
namespace Domain.Interfaces;
public interface IControleChaveService
{
    Task<ApiDataService<List<ControleChaveDto>>> ListarAsync();
    Task<ApiDataService<List<ControleChaveDto>>> ListarMovimentacoesAsync(DateTime inicio, DateTime fim, long? matricula = null);
    Task<ApiDataService<List<int>>> ListarMesesComMovimentacaoAsync(int ano);
    Task<ApiDataService<List<PessoaChaveDto>>> PesquisarPessoasAsync(string? pesquisa);
    Task<ApiDataService<ControleChaveDto>> ObterAsync(int id);
    Task<ApiDataService<ControleChaveDto>> AdicionarAsync(ControleChaveDto dto);
    Task<ApiDataService<ControleChaveDto>> AtualizarAsync(int id, ControleChaveDto dto);
    Task<ApiDataService<ControleChaveDto>> EmprestarAsync(int id, EmprestarChaveDto dto);
    Task<ApiDataService<ControleChaveDto>> DevolverAsync(int id, DevolverChaveDto dto);
    Task<ApiDataService<bool>> ExcluirAsync(int id);
    Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarRelatorioAsync(string formato);
    Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarRelatorioMovimentacoesAsync(string formato, DateTime inicio, DateTime fim, long? matricula = null, IReadOnlyCollection<int>? ids = null);
}
