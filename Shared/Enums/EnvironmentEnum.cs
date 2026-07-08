using System.ComponentModel.DataAnnotations;

namespace Shared.Enums;

public enum EnvironmentEnum
{
    [Display(Name = "DEV", Description = "(Banco Prod / App Teste)")]
    Development,

    [Display(Name = "Teste", Description = "(Banco Teste / App Teste)")]
    Test,

    [Display(Name = "Homologação", Description = "(Banco Teste / App Prod)")]
    Staging,

    [Display(Name = "Produção", Description = "(Banco Prod / App Prod)")]
    Production
}
