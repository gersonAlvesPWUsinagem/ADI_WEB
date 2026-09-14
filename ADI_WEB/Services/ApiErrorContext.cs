using Domain.Interfaces;
using Microsoft.AspNetCore.Components;

namespace ADI_WEB.Services;

public class ApiErrorContext : IApiErrorContext
{
    private readonly NavigationManager _navigation;
    public ApiErrorContext(NavigationManager navigation) => _navigation = navigation;

    public string ObterTelaAtual()
    {
        var rota = _navigation.ToBaseRelativePath(_navigation.Uri).Split('?', '#')[0].Trim('/');
        return string.IsNullOrWhiteSpace(rota) ? "/" : "/" + rota;
    }
}
