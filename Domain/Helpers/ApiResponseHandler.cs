using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    /// <summary>
    /// Classe utilitária para padronizar o tratamento de respostas HTTP vindas de APIs externas.
    /// </summary>
    public static class ApiResponseHandler
    {
        /// <summary>
        /// Processa o conteúdo de uma <see cref="HttpResponseMessage"/> e o converte para um objeto <see cref="ApiDataService{T}"/>.
        /// Este método lida com JSONs que já possuem o envelope de dados ou JSONs que contêm apenas o objeto bruto.
        /// </summary>
        /// <typeparam name="T">O tipo de dado esperado no campo 'Value' do retorno.</typeparam>
        /// <param name="response">A resposta HTTP recebida do HttpClient.</param>
        /// <param name="customErrorMessage">Mensagem opcional de erro caso a operação falhe.</param>
        /// <param name="customSuccessMessage">Mensagem opcional de sucesso para operações positivas.</param>
        /// <returns>Uma instância de <see cref="ApiDataService{T}"/> contendo os dados, status e mensagens de retorno.</returns>
        public static async Task<ApiDataService<T>> HandleResponseAsync<T>(
            HttpResponseMessage response,
            string customErrorMessage = null!,
            string customSuccessMessage = null!)
        {
            try
            {
                var json = await response.Content.ReadAsStringAsync();
                var statusCode = (int)response.StatusCode;
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (!response.IsSuccessStatusCode)
                {
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        // Verifica se o conteúdo realmente parece um JSON antes de tentar o Parse
                        if (json.Trim().StartsWith("{"))
                        {
                            try
                            {
                                using var errorDoc = JsonDocument.Parse(json);
                                var errorRoot = errorDoc.RootElement;

                                if (errorRoot.TryGetProperty("Message", out var msgProp) || errorRoot.TryGetProperty("message", out msgProp))
                                {
                                    var msg = msgProp.GetString();
                                    int apiCode = errorRoot.TryGetProperty("StatusCode", out var codeProp) ? codeProp.GetInt32() : statusCode;
                                    throw new ApiException(apiCode, msg ?? "Fatal erro!");
                                }
                            }
                            catch (ApiException) { throw; }
                            catch { /* Se não for JSON válido, cai no throw abaixo */ }
                        }
                    }

                    // Se o código chegou aqui, o erro veio como string bruta (aquela que você postou)
                    // Vamos tentar extrair a primeira linha (que contém a mensagem de erro)
                    var clearMessage = json.Split('\n')[0].Replace("Domain.Exceptions.ApiException:", "").Trim();
                    throw new ApiException(statusCode, (string.IsNullOrWhiteSpace(clearMessage) ? response.ReasonPhrase : clearMessage ?? "Fatal error") ?? "Fatal erro!");
                }

                // 2. Tratamento para respostas de SUCESSO vazias
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new ApiDataService<T>(default!, true, customSuccessMessage, statusCode);
                }

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                // 3. CENÁRIO: O JSON de SUCESSO é o envelope ApiDataService
                if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("Success", out _))
                {
                    var apiResult = JsonSerializer.Deserialize<ApiDataService<T>>(json, options);

                    // Se o backend mandou Success = false mesmo com HTTP 200 (raro, mas possível)
                    if (apiResult != null && !apiResult.Success)
                    {
                        throw new ApiException(apiResult.StatusCode, apiResult.Message ?? "Fatal erro!");
                    }

                    return apiResult!;
                }

                // 4. CENÁRIO: O JSON é o objeto bruto (T)
                var resultObj = JsonSerializer.Deserialize<T>(json, options);
                return new ApiDataService<T>(resultObj!, true, customSuccessMessage, statusCode);
            }
            catch (ApiException ex)
            {
                return new ApiDataService<T>(
                        default!,
                        false,
                        ex.Message,
                        ex.ErrorCode
                    );

            } // Garante que ApiException passe direto para quem chamou
            catch (Exception ex)
            {
                return new ApiDataService<T>(
                    default!,
                    false,
                    $"Erro técnico no processamento: {ex.Message}",
                    500
                );
            }
        }
    }
}
