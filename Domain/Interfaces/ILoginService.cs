using Domain.Dtos.Login;
using Domain.Dtos.Permission;
using Domain.Helpers;

namespace Domain.Interfaces
{
    public interface ILoginService
    {
        Task<ApiDataService<string>> FazerLoginAsync(UserDto user);
        Task<ApiDataService<List<PermissionUserDto>>> ValidarPermissaoAsync(Shared.Enums.ModuloEnum modulo, int permission);
    }
}
