using Domain.Dtos.ApiErrors;
using Domain.Helpers;

namespace Domain.Interfaces;

public interface IApiErrorLogService
{
    Task<ApiDataService<List<ApiErrorLogDto>>> ListarAsync(DateTime data, string? usuarioOuMatricula = null);
}
