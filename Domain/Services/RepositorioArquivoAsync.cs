using Domain.Dtos.Login;
using Domain.Dtos.Permission;
using Domain.Helpers;
using Domain.Interfaces;

namespace Domain.Services
{
    public class RepositorioArquivoAsync : BaseHttpService, IRepositorioArquivoAsync
    {


        // O HttpClient já vem configurado com a BaseURL correta graças ao Typed Client
        public RepositorioArquivoAsync(HttpClient httpClient, IDataSessionHelper dataSession, IApiErrorContext errorContext)
            : base(httpClient, dataSession, errorContext)
        {
        }

        public async Task<ApiDataService<string>> LoadingPage()
        {
            string url = $"UploadFileDownloadFile";

            var result = await GetAsync<string>(url);

            return result;
        }
    }
}
