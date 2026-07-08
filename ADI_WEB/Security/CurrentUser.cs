using Shared.Enums;

namespace ADI_WEB.Security;

/// <summary>
/// Represents the authenticated ADI user using values read directly from JWT claims.
/// </summary>
public sealed class CurrentUser
{
    /// <summary>Gets or sets the employee registration.</summary>
    public int Matricula { get; set; }

    /// <summary>Gets or sets the ADI identifier.</summary>
    public decimal IdAdi { get; set; }

    /// <summary>Gets or sets the login.</summary>
    public string? Login { get; set; }

    /// <summary>Gets or sets the display name.</summary>
    public string? Name { get; set; }

    /// <summary>Gets or sets the permission level.</summary>
    public string? Level { get; set; }

    /// <summary>Gets or sets the company.</summary>
    public int Empresa { get; set; }

    /// <summary>Gets or sets the branch.</summary>
    public int Filial { get; set; }

    /// <summary>Gets or sets the establishment.</summary>
    public int Estabelecimento { get; set; }

    /// <summary>Gets or sets the environment.</summary>
    public EnvironmentEnum Environment { get; set; }

    /// <summary>Gets or sets the platform.</summary>
    public string? Plataforma { get; set; }

    /// <summary>Gets or sets the target OS type.</summary>
    public TargetOSTypeEnum TargetOSType { get; set; }

    /// <summary>Gets or sets the application version.</summary>
    public string? Version { get; set; }

    /// <summary>Gets or sets the MAC address.</summary>
    public string? MacAddress { get; set; }

    /// <summary>Gets or sets the Android or asset identifier.</summary>
    public string? Android { get; set; }

    /// <summary>Gets or sets the IPv4 address.</summary>
    public string? IpV4 { get; set; }

    /// <summary>Gets or sets the quantity claim.</summary>
    public int Quantidade { get; set; }

    /// <summary>Gets or sets the new version claim.</summary>
    public string? NovaVersao { get; set; }

    /// <summary>Gets or sets a value indicating whether the update is mandatory.</summary>
    public bool IsObrigatorio { get; set; }

    /// <summary>Gets or sets a value indicating whether the principal is authenticated.</summary>
    public bool IsAuthenticated { get; set; }
}
