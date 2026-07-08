using Shared.Enums;
using System.Runtime.InteropServices;

namespace Domain.Helpers;

/// <summary>
/// Classe responsável por obter informações sobre o sistema operacional em que a aplicação está sendo executada.
/// </summary>
public static class SystemInfoHelper
{
    /// <summary>
    /// Obtém o tipo de sistema operacional.
    /// </summary>
    /// <returns>Retorna uma enum indicando o sistema operacional (Windows, Linux, macOS, Android ou Unknown OS).</returns>
    public static TargetOSTypeEnum GetOperatingSystem()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return TargetOSTypeEnum.Windows;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return TargetOSTypeEnum.Linux;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return TargetOSTypeEnum.MacOS;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID")))
        {
            return TargetOSTypeEnum.Android;
        }
        else
        {
            return TargetOSTypeEnum.Unknown;
        }
    }


}
