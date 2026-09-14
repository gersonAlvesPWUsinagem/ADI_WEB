using Domain.Dtos.ApiErrors;
using Domain.Helpers;

namespace Domain.Interfaces;

public interface IApiErrorLogService
{
    Task<ApiDataService<List<ApiErrorPessoaDto>>> PesquisarPessoasAsync(string? pesquisa);
    Task<ApiDataService<List<ApiErrorLogDto>>> ListarAsync(
        DateTime dataInicial,
        DateTime dataFinal,
        string? usuarioOuMatricula = null);
}
