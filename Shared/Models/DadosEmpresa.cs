using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class DadosEmpresa
    {
        public int Empresa { get; set; }
        public int Filial { get; set; }
        public int Estabelecimento { get; set; }
        public long EmpFill
        {
            get { return (long)Empresa * 10000 + Filial; }
        }
    }
}
