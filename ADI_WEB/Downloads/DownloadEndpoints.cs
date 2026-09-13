namespace ADI_WEB.Downloads;

public static class DownloadEndpoints
{
    public static void MapDownloads(this WebApplication app)
    {
        app.MapGet("/downloads/file/{**path}", (string path, DownloadStorage storage, HttpContext context) =>
        {
            try
            {
                var fullPath = storage.Resolve(path);
                if (!DownloadStorage.IsPublicFile(System.IO.Path.GetFileName(fullPath)) || !File.Exists(fullPath))
                    return Results.NotFound();
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                var type = fullPath.EndsWith(".apk", StringComparison.OrdinalIgnoreCase)
                    ? "application/vnd.android.package-archive" : "application/octet-stream";
                return Results.File(fullPath, type, System.IO.Path.GetFileName(fullPath), enableRangeProcessing: true);
            }
            catch (ArgumentException) { return Results.BadRequest(); }
            catch (IOException) { return Results.StatusCode(503); }
            catch (UnauthorizedAccessException) { return Results.StatusCode(503); }
        }).AllowAnonymous();

        // Preserve the API response envelope and mandatory-update semantics for mobile clients.
        app.MapGet("/updates/coletor/check", async (int targetOS, int major, int minor, int patch,
            IHttpClientFactory clients, HttpContext context) =>
        {
            context.Response.Headers.CacheControl = "no-store";
            if (targetOS != 3 || major < 0 || minor < 0 || patch < 0) return Results.BadRequest();
            try
            {
                using var client = clients.CreateClient("DownloadsApi");
                using var response = await client.GetAsync(
                    $"AppVersions/get-validar-versao?targetOS={targetOS}&major={major}&minor={minor}&patch={patch}", context.RequestAborted);
                var json = await response.Content.ReadAsStringAsync(context.RequestAborted);
                return Results.Content(json, "application/json", statusCode: (int)response.StatusCode);
            }
            catch (HttpRequestException) { return Results.StatusCode(503); }
            catch (TaskCanceledException) { return Results.StatusCode(504); }
        }).AllowAnonymous();
    }
}
