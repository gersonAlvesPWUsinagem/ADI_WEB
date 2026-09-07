using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Helpers;

/// <summary>
/// Classe responsável por encapsular dados de resposta de uma API, incluindo mensagens de erro e status de erro.
/// </summary>
/// <typeparam name="T">O tipo de dado encapsulado pela resposta da API.</typeparam>
public class ApiDataService<T>
{
    /// <summary>
    /// Obtém ou define os dados da resposta da API.
    /// </summary>
    [JsonPropertyName("Value")]
    public T? Value { get; set; }

    /// <summary>
    /// Obtém ou define a mensagem de erro associada à resposta da API.
    /// </summary>
    [JsonPropertyName("Message")]
    public string? Message { get; set; }

    /// <summary>
    /// Obtém ou define um valor que indica se ocorreu um erro na resposta da API.
    /// </summary>
    [JsonPropertyName("Success")]
    public bool Success { get; set; }

    [JsonPropertyName("StatusCode")]
    public int StatusCode { get; set; }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="ApiDataService{T}"/>.
    /// </summary>
    public ApiDataService() { }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="ApiDataService{T}"/> com os dados, status de erro e mensagem fornecidos.
    /// </summary>
    /// <param name="value">Os dados da resposta da API.</param>
    /// <param name="success">Indica se ocorreu sucesso na resposta da API.</param>
    /// <param name="message">A mensagem associada à resposta da API.</param>
    /// <param name="statusCode">O código de status HTTP.</param>
    public ApiDataService(T value, bool success, string message, int statusCode)
    {
        Value = value == null ? EnsureDataInstance(value) : value;

        // Proteção contra NullReferenceException caso a mensagem venha nula
        Message = string.IsNullOrEmpty(message) ? string.Empty : message;

        Success = success;
        StatusCode = StatusCode; // Mantido exatamente como no original
    }

    /// <summary>
    /// Garante que a propriedade <see cref="Value"/> seja instanciada se aplicável, 
    /// tratando com segurança tipos primitivos como string que não suportam Activator.CreateInstance.
    /// </summary>
    /// <param name="data">Os dados da resposta da API.</param>
    /// <returns>Retorna uma instância válida do tipo <typeparamref name="T"/>.</returns>
    private T EnsureDataInstance(T data)
    {
        // Se o tipo for string, retorna string.Empty de forma segura sem estourar o Activator
        if (typeof(T) == typeof(string))
        {
            return (T)(object)string.Empty;
        }

        // Para os demais tipos complexos em uso na produção, o comportamento original é mantido
        return Activator.CreateInstance<T>()!;
    }
}