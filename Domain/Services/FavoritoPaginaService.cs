using Domain.Dtos.Favoritos;
using Domain.Helpers;
using Domain.Interfaces;
namespace Domain.Services;
public class FavoritoPaginaService : BaseHttpService, IFavoritoPaginaService
{
    public FavoritoPaginaService(HttpClient httpClient, IDataSessionHelper dataSession) : base(httpClient, dataSession) { }
    public Task<ApiDataService<List<FavoritoPaginaDto>>> ListarAsync() => GetAsync<List<FavoritoPaginaDto>>("favoritos");
    public Task<ApiDataService<FavoritoPaginaDto>> AlternarAsync(FavoritoPaginaDto dto) => PostAsync<FavoritoPaginaDto>("favoritos/alternar", dto);
}
