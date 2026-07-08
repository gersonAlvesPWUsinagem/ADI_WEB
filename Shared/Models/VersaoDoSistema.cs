using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class VersaoDoSistema
    {
        public string? VersaoAtual { get; set; }
        public string? NovaVersao { get; set; }
        public int Quantidade { get; set; }
        public bool IsObrigatoria { get; set; }
        public string? MacAddress { get; set; }
        public string? IpV4 { get; set; }
        public string? Android { get; set; }
        public string? Plataforma { get; set; }
    }
}
