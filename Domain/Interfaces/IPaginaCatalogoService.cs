using Domain.Dtos.Paginas;
using Domain.Helpers;

namespace Domain.Interfaces;

public interface IPaginaCatalogoService
{
    Task<ApiDataService<List<PaginaCatalogoDto>>> ListarAsync();
    Task<ApiDataService<List<PaginaCatalogoDto>>> CompararAsync(IEnumerable<PaginaCatalogoDto> definicoes);
    Task<ApiDataService<bool>> AtualizarAsync(int id, PaginaCatalogoDto pagina);
    Task<ApiDataService<List<PaginaCatalogoDto>>> SincronizarDesenvolvimentoAsync(IEnumerable<PaginaCatalogoDto> definicoes);
}
