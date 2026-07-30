using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Portaria
{
    public class PortariaOcorrenciaDto
    {
        [Required(ErrorMessage = "O campo Id é obrigatório.")]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Operador1 { get; set; }
        public string? HoraEntrada { get; set; }
        public string? DataApEntrada { get; set; }

        public string? Operador2 { get; set; }
        public string? HoraSaida { get; set; }
        public string? DataApSaida { get; set; }
        [Required(ErrorMessage = "O campo Ocorrencia é obrigatório.")]
        public string? Ocorrencia { get; set; }
    }
}
