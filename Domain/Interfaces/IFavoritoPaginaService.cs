using Domain.Dtos.Favoritos;
using Domain.Helpers;
namespace Domain.Interfaces;
public interface IFavoritoPaginaService
{
    Task<ApiDataService<List<FavoritoPaginaDto>>> ListarAsync();
    Task<ApiDataService<FavoritoPaginaDto>> AlternarAsync(FavoritoPaginaDto dto);
}
