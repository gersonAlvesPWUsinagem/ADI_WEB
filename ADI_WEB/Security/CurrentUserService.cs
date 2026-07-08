using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Enums;

namespace ADI_WEB.Security;

/// <summary>
/// Builds a typed current user from the active <see cref="ClaimsPrincipal"/>.
/// </summary>
public sealed class CurrentUserService : ICurrentUser
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentUserService"/> class.
    /// </summary>
    public CurrentUserService(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    /// <inheritdoc />
    public int Matricula => User.Matricula;

    /// <inheritdoc />
    public decimal IdAdi => User.IdAdi;

    /// <inheritdoc />
    public string? Login => User.Login;

    /// <inheritdoc />
    public string? Name => User.Name;

    /// <inheritdoc />
    public string? Level => User.Level;

    /// <inheritdoc />
    public int Empresa => User.Empresa;

    /// <inheritdoc />
    public int Filial => User.Filial;

    /// <inheritdoc />
    public int Estabelecimento => User.Estabelecimento;

    /// <inheritdoc />
    public EnvironmentEnum Environment => User.Environment;

    /// <inheritdoc />
    public string? Plataforma => User.Plataforma;

    /// <inheritdoc />
    public TargetOSTypeEnum TargetOSType => User.TargetOSType;

    /// <inheritdoc />
    public string? Version => User.Version;

    /// <inheritdoc />
    public string? MacAddress => User.MacAddress;

    /// <inheritdoc />
    public string? Android => User.Android;

    /// <inheritdoc />
    public string? IpV4 => User.IpV4;

    /// <inheritdoc />
    public int Quantidade => User.Quantidade;

    /// <inheritdoc />
    public string? NovaVersao => User.NovaVersao;

    /// <inheritdoc />
    public bool IsObrigatorio => User.IsObrigatorio;

    /// <inheritdoc />
    public bool IsAuthenticated => User.IsAuthenticated;

    /// <inheritdoc />
    public CurrentUser User => BuildUser(_authenticationStateProvider.GetAuthenticationStateAsync().GetAwaiter().GetResult().User);

    private static CurrentUser BuildUser(ClaimsPrincipal principal)
    {
        var isAuthenticated = principal.Identity?.IsAuthenticated == true;

        return new CurrentUser
        {
            IsAuthenticated = isAuthenticated,
            Matricula = GetInt(principal, "Matricula"),
            IdAdi = GetDecimal(principal, "IdAdi"),
            Login = GetString(principal, "Login"),
            Name = GetString(principal, "Name"),
            Level = GetString(principal, "Level"),
            Empresa = GetInt(principal, "Empresa"),
            Filial = GetInt(principal, "Filial"),
            Estabelecimento = GetInt(principal, "Estabelecimento"),
            Environment = EnvironmentClaimFormatter.ParseOrDefault(GetString(principal, "Environment")),
            Plataforma = GetString(principal, "Plataforma"),
            TargetOSType = (TargetOSTypeEnum)GetInt(principal, "TargetOSType"),
            Version = GetString(principal, "Version"),
            MacAddress = GetString(principal, "MacAddress"),
            Android = GetString(principal, "Android"),
            IpV4 = GetString(principal, "IpV4"),
            Quantidade = GetInt(principal, "Quantidade"),
            NovaVersao = GetString(principal, "NovaVersao"),
            IsObrigatorio = GetString(principal, "IsObrigatorio") == "1" || GetString(principal, "IsObrigatoro") == "1"
        };
    }

    private static string? GetString(ClaimsPrincipal principal, string claimType)
    {
        return principal.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
    }

    private static int GetInt(ClaimsPrincipal principal, string claimType)
    {
        return int.TryParse(GetString(principal, claimType), out var value) ? value : 0;
    }

    private static decimal GetDecimal(ClaimsPrincipal principal, string claimType)
    {
        return decimal.TryParse(GetString(principal, claimType), out var value) ? value : 0;
    }
}
