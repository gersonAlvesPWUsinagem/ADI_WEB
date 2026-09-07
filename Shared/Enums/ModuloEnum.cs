using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Enums
{
    public enum ModuloEnum
    {
        [Display(Name = "PADRÃO / NÃO DEFINIDO")]
        Default = 0,

        [Display(Name = "COMERCIAL")]
        Comercial = 1,

        [Display(Name = "FINANCEIRO")]
        Financeiro = 2,

        [Display(Name = "QUALIDADE")]
        Qualidade = 3,

        [Display(Name = "ENGENHARIA")]
        Engenharia = 4,

        [Display(Name = "MANUFATURA")]
        Manufatura = 5,

        [Display(Name = "SUPRIMENTOS")]
        Suprimentos = 6
    }
}
