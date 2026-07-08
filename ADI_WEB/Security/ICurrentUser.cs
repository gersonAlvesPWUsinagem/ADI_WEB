using Shared.Enums;

namespace ADI_WEB.Security;

/// <summary>
/// Exposes the authenticated ADI user to application services.
/// </summary>
public interface ICurrentUser
{
    /// <summary>Gets the employee registration.</summary>
    int Matricula { get; }

    /// <summary>Gets the ADI identifier.</summary>
    decimal IdAdi { get; }

    /// <summary>Gets the login.</summary>
    string? Login { get; }

    /// <summary>Gets the display name.</summary>
    string? Name { get; }

    /// <summary>Gets the permission level.</summary>
    string? Level { get; }

    /// <summary>Gets the company.</summary>
    int Empresa { get; }

    /// <summary>Gets the branch.</summary>
    int Filial { get; }

    /// <summary>Gets the establishment.</summary>
    int Estabelecimento { get; }

    /// <summary>Gets the environment.</summary>
    EnvironmentEnum Environment { get; }

    /// <summary>Gets the platform.</summary>
    string? Plataforma { get; }

    /// <summary>Gets the target OS type.</summary>
    TargetOSTypeEnum TargetOSType { get; }

    /// <summary>Gets the application version.</summary>
    string? Version { get; }

    /// <summary>Gets the MAC address.</summary>
    string? MacAddress { get; }

    /// <summary>Gets the Android or asset identifier.</summary>
    string? Android { get; }

    /// <summary>Gets the IPv4 address.</summary>
    string? IpV4 { get; }

    /// <summary>Gets the quantity claim.</summary>
    int Quantidade { get; }

    /// <summary>Gets the new version claim.</summary>
    string? NovaVersao { get; }

    /// <summary>Gets a value indicating whether the update is mandatory.</summary>
    bool IsObrigatorio { get; }

    /// <summary>Gets a value indicating whether the user is authenticated.</summary>
    bool IsAuthenticated { get; }

    /// <summary>Gets the current user model.</summary>
    CurrentUser User { get; }
}
