using Domain.Dtos.AgenteLocal;
using Domain.Interfaces;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Domain.Services
{
    public class AgenteLocalService : IAgenteLocalService
    {
        private readonly IJSRuntime _jsRuntime;

        // Injetamos o IJSRuntime em vez do HttpClient
        public AgenteLocalService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<AgenteLocalDto?> ObterDadosMaquinaAsync()
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(4));

            try
            {
                // Invoca a função que criamos no arquivo JS passando o token de cancelamento
                var jsonResultado = await _jsRuntime.InvokeAsync<string>(
                    "agenteLocal.obterDadosMaquina",
                    cts.Token
                );

                if (string.IsNullOrEmpty(jsonResultado))
                    return null;

                // Deserializa mantendo os mesmos nomes das propriedades retornadas pelo agente
                return JsonSerializer.Deserialize<AgenteLocalDto>(jsonResultado, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (OperationCanceledException)
            {
                throw new HttpRequestException("O agente local demorou muito para responder no navegador do cliente (Timeout de 4s).");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Erro ao tentar invocar a ponte JavaScript com o agente: {ex.Message}");
            }
        }
    }
}