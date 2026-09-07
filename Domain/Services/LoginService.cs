using Domain.Dtos.Login;
using Domain.Dtos.Permission;
using Domain.Helpers;
using Domain.Interfaces;

namespace Domain.Services
{
    public class LoginService : BaseHttpService, ILoginService
    {


        // O HttpClient já vem configurado com a BaseURL correta graças ao Typed Client
        public LoginService(HttpClient httpClient, IDataSessionHelper dataSession)
            : base(httpClient, dataSession)
        {
        }

        public async Task<ApiDataService<string>> FazerLoginAsync(UserDto user)
        {
            var result = await PostAsync<string>("Auth", user);
            return result;
        }

        public async Task<ApiDataService<List<PermissionUserDto>>> ValidarPermissaoAsync(Shared.Enums.ModuloEnum modulo, int permission)
        {
            string url = $"Auth/get-permission-user?modulo={modulo}&permission={permission}";

            var result = await GetAsync<List<PermissionUserDto>>(url);

            return result;
        }
    }
}