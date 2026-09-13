using System.Net.Http.Headers;
using System.Net.Http.Json;
using Domain.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace ADI_WEB.Downloads;

public sealed record PublicationResult(bool Success, string? Message, string? FileName, string? Version);

public sealed class DownloadPublisher(DownloadStorage storage, IHttpClientFactory clients,
    AuthenticationStateProvider authentication, IDataSessionHelper session)
{
    private async Task EnsureAuthenticatedAsync()
    {
        var state = await authentication.GetAuthenticationStateAsync();
        if (state.User.Identity?.IsAuthenticated != true)
            throw new UnauthorizedAccessException("Entre no sistema para publicar arquivos.");
    }

    public async Task SaveAsync(string directory, string name, Stream stream, long length, bool create)
    {
        await EnsureAuthenticatedAsync();
        await storage.SaveAsync(directory, name, stream, length, create);
    }

    public async Task<PublicationResult> PublishCollectorAsync(Stream stream, string name, int increment,
        string notes, bool mandatory)
    {
        await EnsureAuthenticatedAsync();
        using var client = clients.CreateClient("DownloadsApi");
        using var form = new MultipartFormDataContent();
        using var content = new StreamContent(stream);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.android.package-archive");
        form.Add(content, "File", name);
        form.Add(new StringContent("1"), "Plataforma"); // COLETOR
        form.Add(new StringContent("3"), "TargetOSType"); // Android
        form.Add(new StringContent(increment.ToString()), "VersionIncrementType");
        form.Add(new StringContent(notes), "ReleaseNotes");
        form.Add(new StringContent(mandatory.ToString()), "IsObrigatoria");
        using var request = new HttpRequestMessage(HttpMethod.Post, "AppVersions/upload-apk") { Content = form };
        var token = session.DataSession?.DadosToken?.Token;
        if (!string.IsNullOrWhiteSpace(token)) request.Headers.Authorization = new("Bearer", token);
        using var response = await client.SendAsync(request);
        PublicationResult? result;
        try { result = await response.Content.ReadFromJsonAsync<PublicationResult>(); }
        catch (System.Text.Json.JsonException) { throw new IOException($"A API não conseguiu publicar o arquivo (HTTP {(int)response.StatusCode})."); }
        if (!response.IsSuccessStatusCode || result?.Success != true)
            throw new IOException(result?.Message ?? $"Falha no upload (HTTP {(int)response.StatusCode}).");
        return result;
    }
}
