using Domain.Dtos.ApiErrors;
using Domain.Helpers;
using Domain.Interfaces;

namespace Domain.Services;

public class ApiErrorLogService : BaseHttpService, IApiErrorLogService
{
    public ApiErrorLogService(HttpClient httpClient, IDataSessionHelper dataSession, IApiErrorContext errorContext)
        : base(httpClient, dataSession, errorContext) { }

    public Task<ApiDataService<List<ApiErrorLogDto>>> ListarAsync(DateTime data, string? usuarioOuMatricula = null)
    {
        var url = $"api-errors?data={data:yyyy-MM-dd}";
        if (!string.IsNullOrWhiteSpace(usuarioOuMatricula))
            url += $"&usuarioOuMatricula={Uri.EscapeDataString(usuarioOuMatricula.Trim())}";
        return GetAsync<List<ApiErrorLogDto>>(url);
    }
}
