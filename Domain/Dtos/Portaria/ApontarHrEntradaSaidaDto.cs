using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Portaria
{
    public class ApontarHrEntradaSaidaDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int PessoaId { get; set; }
        public string? Operador1 { get; set; }
        public string? HoraEntrada { get; set; }
        public string? DataScEntrada { get; set; }

        public string? Operador2 { get; set; }
        public string? HoraSaida { get; set; }
        public string? DataScSaida { get; set; }
        public int TypeEvent { get; set; }
        public string? DataOcorrencia { get; set; }
    }
}
