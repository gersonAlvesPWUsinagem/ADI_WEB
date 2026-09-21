using ADI_WEB.Components;
using ADI_WEB.Components.SharedComps;
using ADI_WEB.Downloads;
using ADI_WEB.Security;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Services;
using MudBlazor.Services;
using ADI_WEB.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<DownloadStorage>();
builder.Services.AddScoped<DownloadPublisher>();
builder.Services.AddHttpClient("DownloadsApi", client =>
{
    client.BaseAddress = new Uri((builder.Configuration["Downloads:ApiBaseUrl"] ?? Servidores.GetBaseUrl()).TrimEnd('/') + "/");
    client.Timeout = TimeSpan.FromMinutes(30);
});

#region [ Configurações de Framework e Interface (Blazor & MudBlazor) ]
// Injeta componentes do MudBlazor UI
builder.Services.AddMudServices();

// Permite acessar o contexto HTTP (Cookies, Headers, Claims) em classes de serviço
builder.Services.AddHttpContextAccessor();

// Configura o Blazor Server com componentes interativos
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
#endregion

#region [ Configuração do HttpClient Base da Aplicação ]
// Registra uma instância global do HttpClient configurada com a URL Base da API.
// Qualquer serviço injetado via DI usará essa mesma base sem precisar reconfigurar.
builder.Services.AddScoped(sp =>
{
    var primaryHandler = new HttpClientHandler
    {
        // Ignora a validação do certificado SSL (útil para desenvolvimento local e redes internas)
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    };

    var authenticationFailureHandler = sp.GetRequiredService<ApiAuthenticationFailureHandler>();
    authenticationFailureHandler.InnerHandler = primaryHandler;

    return new HttpClient(authenticationFailureHandler)
    {
        BaseAddress = new Uri(Servidores.GetBaseUrl())
    };
});
#endregion

#region [ Injeção de Dependência - Serviços de Negócio (Domain Services) ]
// Registre aqui todos os serviços da aplicação (Interface -> Implementação Concreta)
builder.Services.AddScoped<IDataSessionHelper, DataSessionHelper>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IAgenteLocalService, AgenteLocalService>();
builder.Services.AddScoped<IGeradorTermoService, GeradorTermoService>();
builder.Services.AddScoped<IPortariaService, PortariaService>();
builder.Services.AddScoped<IRamalService, RamalService>();
builder.Services.AddScoped<IFavoritoPaginaService, FavoritoPaginaService>();
builder.Services.AddScoped<IApiErrorLogService, ApiErrorLogService>();
builder.Services.AddScoped<IApiErrorContext, ApiErrorContext>();
builder.Services.AddScoped<IControleChaveService, ControleChaveService>();
builder.Services.AddScoped<ICatracaService, CatracaService>();
builder.Services.AddScoped<IRepositorioArquivoAsync, RepositorioArquivoAsync>();
builder.Services.AddScoped<FavoritoPaginaState>();
builder.Services.AddScoped<OperadorPortariaState>();
builder.Services.AddScoped<SessionExpirationState>();
builder.Services.AddScoped<CatracaNavegacaoState>();
builder.Services.AddScoped<ApiAuthenticationFailureHandler>();

builder.Services.AddScoped<SnackbarComp>();
builder.Services.AddScoped<LoadingHelper>();

builder.Services.AddSingleton<AtualizacaoPortariaNotifier>();

builder.Services.AddHostedService<GerarApontamentosDiariosHostedService>();
#endregion

#region [ Autenticação e Autorização (JWT via Cookie) ]
builder.Services.AddJwtCookieAuthentication(options =>
{
    options.CookieName = "AuthTokenADI";
    options.LoginPath = "/user/login";
    options.ValidateLifetime = true;
    options.ValidateIssuer = false;
    options.ValidateAudience = false;
    options.ValidateSigningKey = false;
});

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
#endregion

// --- CONSTRUÇÃO DO CONTAINER DA APLICAÇÃO ---
var app = builder.Build();

#region [ Pipeline de Tratamento de Erros e Redirecionamentos ]
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
#endregion

#region [ Middlewares de Arquivos Estáticos e Segurança ]
app.UseStaticFiles();
app.UseRouting();

// É crucial que a Autenticação venha ANTES da Autorização no pipeline
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();
#endregion

#region [ Mapeamento de Rotas e Execução da Aplicação ]
app.MapStaticAssets();
app.MapDownloads();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
#endregion
