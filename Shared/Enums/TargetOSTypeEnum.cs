namespace Shared.Enums;

/// <summary>
/// Enumeração que representa os diferentes tipos de sistemas operacionais e navegadores suportados.
/// </summary>
public enum TargetOSTypeEnum
{
    /// <summary>
    /// Representa o sistema operacional Windows.
    /// </summary>
    Windows,

    /// <summary>
    /// Representa o sistema operacional Linux.
    /// </summary>
    Linux,

    /// <summary>
    /// Representa o sistema operacional macOS.
    /// </summary>
    MacOS,

    /// <summary>
    /// Representa o sistema operacional Android.
    /// </summary>
    Android,

    /// <summary>
    /// Representa o sistema operacional OSX.
    /// </summary>
    OSX,

    /// <summary>
    /// Navegador Google Chrome executando a aplicação.
    /// </summary>
    Chrome,

    /// <summary>
    /// Navegador Microsoft Edge executando a aplicação.
    /// </summary>
    Edge,

    /// <summary>
    /// Navegador Mozilla Firefox executando a aplicação.
    /// </summary>
    Firefox,

    /// <summary>
    /// Navegador Apple Safari executando a aplicação.
    /// </summary>
    Safari,

    /// <summary>
    /// Navegador Opera executando a aplicação.
    /// </summary>
    Opera,

    /// <summary>
    /// Representa um sistema operacional ou navegador desconhecido.
    /// </summary>
    Unknown
}