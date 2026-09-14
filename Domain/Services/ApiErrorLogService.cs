using Domain.Dtos.ApiErrors;
using Domain.Helpers;
using Domain.Interfaces;

namespace Domain.Services;

public class ApiErrorLogService : BaseHttpService, IApiErrorLogService
{
    public ApiErrorLogService(HttpClient httpClient, IDataSessionHelper dataSession, IApiErrorContext errorContext)
        : base(httpClient, dataSession, errorContext) { }

    public Task<ApiDataService<List<ApiErrorPessoaDto>>> PesquisarPessoasAsync(string? pesquisa) =>
        GetAsync<List<ApiErrorPessoaDto>>(
            $"api-errors/pessoas?search={Uri.EscapeDataString(pesquisa?.Trim() ?? string.Empty)}");

    public Task<ApiDataService<List<ApiErrorLogDto>>> ListarAsync(
        DateTime dataInicial,
        DateTime dataFinal,
        string? usuarioOuMatricula = null)
    {
        var url = $"api-errors?dataInicial={dataInicial:yyyy-MM-dd}&dataFinal={dataFinal:yyyy-MM-dd}";
        if (!string.IsNullOrWhiteSpace(usuarioOuMatricula))
            url += $"&usuarioOuMatricula={Uri.EscapeDataString(usuarioOuMatricula.Trim())}";
        return GetAsync<List<ApiErrorLogDto>>(url);
    }
}
