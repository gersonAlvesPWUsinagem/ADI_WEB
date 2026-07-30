using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Portaria
{
    public class ColaboradoresControladosDto
    {
        public int EMPFIL { get; set; }
        public int MATRICULA { get; set; }
        public string? TRADUCAO_CC { get; set; }
        public string? NOME { get; set; }
        public string? DEPARTAMENTO { get; set; }
        public bool IsControl { get; set; }
        public int ControladoId { get; set; }
    }
}
