using ADI_WEB.Components;
using ADI_WEB.Components.SharedComps;
using ADI_WEB.Security;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

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
    var handler = new HttpClientHandler
    {
        // Ignora a validação do certificado SSL (útil para desenvolvimento local e redes internas)
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    };

    return new HttpClient(handler)
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

builder.Services.AddScoped<SnackbarComp>();
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
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
#endregion