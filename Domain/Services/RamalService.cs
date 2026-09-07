using Domain.Dtos.Ramais;
using Domain.Helpers;
using Domain.Interfaces;

namespace Domain.Services;

public class RamalService : BaseHttpService, IRamalService
{
    public RamalService(HttpClient httpClient, IDataSessionHelper dataSession) : base(httpClient, dataSession) { }
    public Task<ApiDataService<List<RamalDto>>> ListarAsync() => GetAsync<List<RamalDto>>("ramais");
    public Task<ApiDataService<RamalDto>> AdicionarAsync(RamalDto dto) => PostAsync<RamalDto>("ramais", dto);
    public Task<ApiDataService<RamalDto>> AtualizarAsync(RamalDto dto) => PutAsync<RamalDto>($"ramais/{dto.Id}", dto);
    public Task<ApiDataService<bool>> ExcluirAsync(int id) => DeleteAsync<bool>($"ramais/{id}");
    public Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> GerarRelatorioAsync(string formato) =>
        PostForFileAsync("ramais/relatorio", new { Formato = formato }, $"Lista_Ramais.{(formato == "PDF" ? "pdf" : "xlsx")}");
}
