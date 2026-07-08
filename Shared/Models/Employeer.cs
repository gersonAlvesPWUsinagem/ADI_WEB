using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Employeer
    {
        public int Matricula { get; set; }
        public decimal IdAdi { get; set; }
        public string? Login { get; set; }
        public string? Name { get; set; }
        public string? Level { get; set; }
        public EnvironmentEnum Environment { get; set; }
        public string? Plataforma { get; set; }
        public TargetOSTypeEnum TargetOSType { get; set; }
        public string? MacAddress { get; set; }
        public string? Version { get; set; }
        public string? IpV4 { get; set; }
        public string? Android { get; set; }
        public int Quantidade { get; set; }
        public string? NovaVersao { get; set; }
        public bool IsObrigatoro { get; set; }
        public int Empresa { get; set; }
        public int Filial { get; set; }
        public int Estabelecimento { get; set; }
    }

}
