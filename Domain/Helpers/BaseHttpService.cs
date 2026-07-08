using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public abstract class BaseHttpService
    {
        protected readonly HttpClient _httpClient;
        private readonly IDataSessionHelper _dataSession;

        protected BaseHttpService(HttpClient httpClient, IDataSessionHelper dataSession)
        {
            _httpClient = httpClient;
            _dataSession = dataSession;
        }

        private void SetAuthorizationHeader()
        {
            // Pega o token de forma segura (null-safe)

            var token = _dataSession?.DataSession?.DadosToken?.Token;

            if (!string.IsNullOrWhiteSpace(token))
            {
                // Se existe token, adiciona o Bearer
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                // Se NÃO existe token, remove o header de autorização para esta chamada
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        protected async Task<ApiDataService<T>> GetAsync<T>(string url)
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.GetAsync(url);
                var result = await ApiResponseHandler.HandleResponseAsync<T>(response);

                return result;
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected async Task<ApiDataService<T>> PostAsync<T>(string url, object data)
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.PostAsJsonAsync(url, data);
                return await ApiResponseHandler.HandleResponseAsync<T>(response);
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected async Task<ApiDataService<T>> PutAsync<T>(string url, object data)
        {
            try
            {
                SetAuthorizationHeader();

                var response = await _httpClient.PutAsJsonAsync(url, data);

                return await ApiResponseHandler.HandleResponseAsync<T>(response);
            }
            catch (ApiException) { throw; }
            catch (Exception)
            {
                throw;
            }
        }
        protected async Task<ApiDataService<T>> DeleteAsync<T>(string url)
        {
            try
            {
                SetAuthorizationHeader();

                var response = await _httpClient.DeleteAsync(url);

                return await ApiResponseHandler.HandleResponseAsync<T>(response);
            }
            catch (ApiException) { throw; }
            catch (Exception)
            {
                throw;
            }
        }
        protected async Task<ApiDataService<T>> DeleteAsync<T>(string url, object data)
        {
            try
            {
                SetAuthorizationHeader();

                // O DeleteAsync padrão do HttpClient não aceita body, 
                // por isso usamos o SendAsync com uma HttpRequestMessage manual.
                var request = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = JsonContent.Create(data)
                };

                var response = await _httpClient.SendAsync(request);

                return await ApiResponseHandler.HandleResponseAsync<T>(response);
            }
            catch (ApiException) { throw; }
            catch (Exception)
            {
                throw;
            }
        }
        protected async Task<ApiDataService<T>> PatchAsync<T>(string url, object data)
        {
            try
            {
                SetAuthorizationHeader();

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), url)
                {
                    Content = JsonContent.Create(data)
                };

                var response = await _httpClient.SendAsync(request);

                return await ApiResponseHandler.HandleResponseAsync<T>(response);
            }
            catch (ApiException) { throw; }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
