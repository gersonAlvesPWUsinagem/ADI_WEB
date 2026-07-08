using Domain.Dtos.Login;
using Domain.Helpers;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILoginService
    {
        Task<ApiDataService<string>> FazerLogin(UserDto user);
    }
}
