using ADI_WEB.Components;
using ADI_WEB.Security;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

#region [ Services Configuration ]
builder.Services.AddMudServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<ILoginService, LoginService>(client =>
{
    string apiUrl = Servidores.GetBaseUrl();
    client.BaseAddress = new Uri(apiUrl);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler();
    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
    return handler;
});
#endregion

#region [ Dependency Injection ]
builder.Services.AddScoped<IDataSessionHelper, DataSessionHelper>();
builder.Services.AddScoped<IAgenteLocalService, AgenteLocalService>();
#endregion

#region [ Authentication & Authorization (JWT Cookie) ]
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

var app = builder.Build();

#region [ Configure the HTTP request pipeline ]
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
#endregion

#region [ Standard ASP.NET Core Security Middlewares ]
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();
#endregion

#region [ Mappings & Execution ]
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
#endregion
