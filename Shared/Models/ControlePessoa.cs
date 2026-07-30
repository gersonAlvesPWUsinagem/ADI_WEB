using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class ControlePessoa
    {
        public int ID { get; set; }
        public int EMPFIL { get; set; }
        public int PessoaId { get; set; }
        public string? MATRICULA { get; set; }
        public string? NOME { get; set; }
        public string? TRADUCAO_CC { get; set; }
        public string? SetorName { get; set; }
        public string? HoraEntrada { get; set; }
        public string? HoraSaida { get; set; }
        public string? Operador1 { get; set; }
        public string? Operador2 { get; set; }
        public string? DataOcorrencia { get; set; }
        public string? DataApEntrada { get; set; }
        public string? DataApSaida { get; set; }
        public string? DataScEntrada { get; set; }
        public string? DataScSaida { get; set; }
        public string? Ocorrencia { get; set; }

        public string? Operador1Display => Operador1?.Split(' ')[0];
        public string? Operador2Display => Operador2?.Split(' ')[0];

        [NotMapped]
        public bool OcorrenciaEntrada { get; set; }
        [NotMapped]
        public bool OcorrenciaSaida { get; set; }
    }
}
