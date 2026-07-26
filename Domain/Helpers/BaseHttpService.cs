using Domain.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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

        #region [ Métodos Auxiliares Internos ]

        private void SetAuthorizationHeader()
        {
            var token = _dataSession?.DataSession?.DadosToken?.Token;

            if (!string.IsNullOrWhiteSpace(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        private static string ExtrairNomeArquivoDoHeader(HttpResponseMessage response, string nomeArquivoPadrao)
        {
            var contentDisposition = response.Content.Headers.ContentDisposition;

            if (contentDisposition != null)
            {
                string nome = contentDisposition.FileNameStar ?? contentDisposition?.FileName ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(nome))
                {
                    return nome.Replace("\"", "").Trim();
                }
            }

            return nomeArquivoPadrao;
        }

        #endregion

        #region [ Verbos HTTP para APIs JSON Padronizadas ]

        protected async Task<ApiDataService<T>> GetAsync<T>(string url)
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.GetAsync(url);
                return await ApiResponseHandler.HandleResponseAsync<T>(response);
            }
            catch (ApiException) { throw; }
            catch (Exception) { throw; }
        }

        protected async Task<ApiDataService<T>> PostAsync<T>(string url, object data)
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.PostAsJsonAsync(url, data);
                return await ApiResponseHandler.HandleResponseAsync<T>(response);
            }
            catch (ApiException) { throw; }
            catch (Exception) { throw; }
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
            catch (Exception) { throw; }
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
            catch (Exception) { throw; }
        }

        protected async Task<ApiDataService<T>> DeleteAsync<T>(string url, object data)
        {
            try
            {
                SetAuthorizationHeader();

                var request = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = JsonContent.Create(data)
                };

                var response = await _httpClient.SendAsync(request);
                return await ApiResponseHandler.HandleResponseAsync<T>(response);
            }
            catch (ApiException) { throw; }
            catch (Exception) { throw; }
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
            catch (Exception) { throw; }
        }

        #endregion

        #region [ Verbos HTTP para Download/Streaming de Arquivos Brutos (PDF, Excel, ZIP) ]

        protected async Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>> PostForFileAsync(
        string url,
        object data,
        string nomeArquivoPadrao = "termo.pdf")
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.PostAsJsonAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                    string mimeType = response.Content.Headers.ContentType?.MediaType ?? "application/pdf";
                    string nomeArquivo = ExtrairNomeArquivoDoHeader(response, nomeArquivoPadrao);

                    // Lê se o servidor pediu 'inline' ou 'attachment'
                    var contentDisposition = response.Content.Headers.ContentDisposition;
                    bool isInline = contentDisposition?.DispositionType?.Equals("inline", StringComparison.OrdinalIgnoreCase) ?? true;

                    return new ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>
                    {
                        Success = true,
                        Value = (bytes, mimeType, nomeArquivo, isInline),
                        Message = "Arquivo gerado com sucesso!"
                    };
                }

                string erroDetalhado = await response.Content.ReadAsStringAsync();
                string mensagemErro = !string.IsNullOrWhiteSpace(erroDetalhado)
                    ? erroDetalhado
                    : $"Erro no servidor ao gerar arquivo ({response.StatusCode})";

                return new ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>
                {
                    Success = false,
                    Message = mensagemErro
                };
            }
            catch (Exception ex)
            {
                return new ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo, bool IsInline)>
                {
                    Success = false,
                    Message = $"Erro ao solicitar arquivo: {ex.Message}"
                };
            }
        }

        protected async Task<ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo)>> GetForFileAsync(
            string url,
            string nomeArquivoPadrao = "documento.pdf")
        {
            try
            {
                SetAuthorizationHeader();
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                    string mimeType = response.Content.Headers.ContentType?.MediaType ?? "application/pdf";
                    string nomeArquivo = ExtrairNomeArquivoDoHeader(response, nomeArquivoPadrao);

                    return new ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo)>
                    {
                        Success = true,
                        Value = (bytes, mimeType, nomeArquivo),
                        Message = "Arquivo baixado com sucesso!"
                    };
                }

                string erroDetalhado = await response.Content.ReadAsStringAsync();
                string mensagemErro = !string.IsNullOrWhiteSpace(erroDetalhado)
                    ? erroDetalhado
                    : $"Erro no servidor ao baixar arquivo ({response.StatusCode})";

                return new ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo)>
                {
                    Success = false,
                    Message = mensagemErro
                };
            }
            catch (Exception ex)
            {
                return new ApiDataService<(byte[] Conteudo, string MimeType, string NomeArquivo)>
                {
                    Success = false,
                    Message = $"Erro ao solicitar arquivo: {ex.Message}"
                };
            }
        }

        #endregion
    }
}