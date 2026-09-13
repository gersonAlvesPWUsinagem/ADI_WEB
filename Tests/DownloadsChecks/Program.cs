using ADI_WEB.Downloads;
using System.Net;
using System.Net.Http.Headers;

var root = Path.Combine(Path.GetTempPath(), "adi-download-checks-" + Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(root);
var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
{
    ["Downloads:RootPath"] = root
}).Build();
var storage = new DownloadStorage(config);
int checks = 0;
void Check(bool value, string message)
{
    if (!value) throw new Exception(message);
    checks++;
}
async Task Reject<T>(Func<Task> action) where T : Exception
{
    try { await action(); }
    catch (T) { checks++; return; }
    throw new Exception("Expected " + typeof(T).Name);
}
foreach (var path in new[] { "../outside", "C:/outside", "app/../outside", "app\\outside", "app/file:stream", "DATABASE/file.txt", ".hidden", "CON.txt", "app//file" })
    await Reject<ArgumentException>(() => { storage.Resolve(path); return Task.CompletedTask; });

byte[] bytes = [1, 2, 3, 4];
await storage.SaveAsync("WindowsCard", "setup.exe", new MemoryStream(bytes), bytes.Length, true);
Check(File.ReadAllBytes(Path.Combine(root, "WindowsCard", "setup.exe")).SequenceEqual(bytes), "Upload bytes");
await Reject<IOException>(() => storage.SaveAsync("WindowsCard", "setup.exe", new MemoryStream([9]), 1, false));
Check(File.ReadAllBytes(Path.Combine(root, "WindowsCard", "setup.exe")).SequenceEqual(bytes), "Overwrite protection");
await Reject<IOException>(() => storage.SaveAsync("WindowsCard", "incomplete.exe", new MemoryStream(bytes), 10, false));
Check(!File.Exists(Path.Combine(root, "WindowsCard", "incomplete.exe")), "Partial upload visible");
Check(Directory.GetFiles(Path.Combine(root, "WindowsCard")).Length == 1, "Temporary file cleanup");
await Reject<ArgumentException>(() => storage.SaveAsync("COLETOR", "app.apk", new MemoryStream(bytes), 4, true));
await Reject<ArgumentException>(() => storage.SaveAsync("WindowsCard", "empty.exe", new MemoryStream(), 0, false));
Directory.CreateDirectory(Path.Combine(root, "DATABASE"));
File.WriteAllText(Path.Combine(root, "DATABASE", "SQLITE.db"), "private");
Check(!storage.List(null).Any(x => x.Name == "DATABASE"), "Database directory exposed");
Directory.CreateDirectory(Path.Combine(root, "COLETOR", "2.9.0"));
Directory.CreateDirectory(Path.Combine(root, "COLETOR", "2.10.0"));
File.WriteAllBytes(Path.Combine(root, "COLETOR", "2.10.0", "coletor.apk"), bytes);
File.WriteAllText(Path.Combine(root, "COLETOR", "2.10.0", "index.html"), "<script>bad()</script>");
Check(storage.List("COLETOR")[0].Name == "2.10.0", "Semantic version ordering");
Check(storage.List("COLETOR/2.10.0").Count == 1, "HTML should not be listed");

var builder = WebApplication.CreateBuilder();
builder.Logging.ClearProviders();
builder.WebHost.UseUrls("http://127.0.0.1:0");
builder.Services.AddSingleton(storage);
builder.Services.AddSingleton<IHttpClientFactory, FakeClientFactory>();
await using var app = builder.Build();
app.MapDownloads();
await app.StartAsync();
using var client = new HttpClient { BaseAddress = new Uri(app.Urls.Single()) };
var file = await client.GetAsync("downloads/file/COLETOR/2.10.0/coletor.apk");
Check(file.IsSuccessStatusCode, "Public APK download");
Check((await file.Content.ReadAsByteArrayAsync()).SequenceEqual(bytes), "Downloaded bytes");
Check(file.Content.Headers.ContentDisposition?.DispositionType == "attachment", "Attachment disposition");
using var request = new HttpRequestMessage(HttpMethod.Get, "downloads/file/COLETOR/2.10.0/coletor.apk");
request.Headers.Range = new RangeHeaderValue(1, 2);
var partial = await client.SendAsync(request);
Check(partial.StatusCode == HttpStatusCode.PartialContent, "Resumable download");
Check((await partial.Content.ReadAsByteArrayAsync()).SequenceEqual(new byte[] { 2, 3 }), "Range bytes");
Check((await client.GetAsync("downloads/file/DATABASE/SQLITE.db")).StatusCode == HttpStatusCode.BadRequest, "Private database endpoint");
Check((await client.GetAsync("downloads/file/COLETOR/2.10.0/index.html")).StatusCode == HttpStatusCode.NotFound, "Generated HTML not executable via file endpoint");
var version = await client.GetAsync("updates/coletor/check?targetOS=3&major=1&minor=0&patch=0");
Check(await version.Content.ReadAsStringAsync() == FakeHandler.Envelope, "Version envelope preserved");
Check(version.Headers.CacheControl?.NoStore == true, "Version check cache");
Check((await client.GetAsync("updates/coletor/check?targetOS=3&major=-1&minor=0&patch=0")).StatusCode == HttpStatusCode.BadRequest, "Invalid version rejected");
await app.StopAsync();
Console.WriteLine($"PASS: {checks} download checks. Temporary fixtures: {root}");

sealed class FakeClientFactory : IHttpClientFactory
{
    public HttpClient CreateClient(string name) => new(new FakeHandler()) { BaseAddress = new Uri("https://api.example/api/") };
}
sealed class FakeHandler : HttpMessageHandler
{
    public const string Envelope = "{\"Success\":true,\"StatusCode\":201,\"Value\":{\"VersionString\":\"2.10.0\",\"IS_OBRIGATORIA\":true}}";
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri?.AbsolutePath != "/api/AppVersions/get-validar-versao") throw new Exception("Wrong upstream endpoint");
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(Envelope) });
    }
}
