using Domain.Dtos.Ramais;
using Domain.Helpers;

namespace Domain.Interfaces;

public interface IRamalService
{
    Task<ApiDataService<List<RamalDto>>> ListarAsync();
    Task<ApiDataService<RamalDto>> AdicionarAsync(RamalDto dto);
    Task<ApiDataService<RamalDto>> AtualizarAsync(RamalDto dto);
    Task<ApiDataService<bool>> ExcluirAsync(int id);
    Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarRelatorioAsync(string formato);
}
