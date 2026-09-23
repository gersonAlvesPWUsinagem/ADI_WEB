using Domain.Dtos.Paginas;
using Domain.Helpers;
using Domain.Interfaces;

namespace Domain.Services;

public class PaginaCatalogoService : BaseHttpService, IPaginaCatalogoService
{
    public PaginaCatalogoService(HttpClient httpClient, IDataSessionHelper dataSession, IApiErrorContext errorContext)
        : base(httpClient, dataSession, errorContext) { }

    public Task<ApiDataService<List<PaginaCatalogoDto>>> ListarAsync() =>
        GetAsync<List<PaginaCatalogoDto>>("paginas-catalogo");

    public Task<ApiDataService<List<PaginaCatalogoDto>>> CompararAsync(IEnumerable<PaginaCatalogoDto> definicoes) =>
        PostAsync<List<PaginaCatalogoDto>>("paginas-catalogo/comparar", definicoes.ToList());

    public Task<ApiDataService<bool>> AtualizarAsync(int id, PaginaCatalogoDto pagina) =>
        PutAsync<bool>($"paginas-catalogo/{id}", pagina);

    public Task<ApiDataService<List<PaginaCatalogoDto>>> SincronizarDesenvolvimentoAsync(IEnumerable<PaginaCatalogoDto> definicoes) =>
        PostAsync<List<PaginaCatalogoDto>>("paginas-catalogo/sincronizar-desenvolvimento", definicoes.ToList());
}
