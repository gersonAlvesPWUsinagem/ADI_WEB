using Domain.Dtos.Login;
using Domain.Helpers;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class LoginService : BaseHttpService, ILoginService 
    {


        // O HttpClient já vem configurado com a BaseURL correta graças ao Typed Client
        public LoginService(HttpClient httpClient, IDataSessionHelper dataSession)
            : base(httpClient, dataSession)
        {
        }

        public async Task<ApiDataService<string>> FazerLogin(UserDto user)
        {
            var result = await PostAsync<string>("Auth", user);
            return result;           
        }
    }
}
