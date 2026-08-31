using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Portaria
{
    public class ResistrarEntradaSaidaDto
    {
        public int ID { get; set; }
        public string? Nome { get; set; }
        public int Pessoa_Cod { get; set; }
        public string? Matricula { get; set; }
        public int Codigo_OP { get; set; }
        public string? HoraDaAcao { get; set; }
        public string? DataAcao { get; set; }
        public int Emp_Fil { get; set; }
        public TipoAcaoPortariaEnum TipoAcaoPortaria { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public string? TRADUCAO_CC { get; set; }
        public string? SetorName { get; set; }
        public bool PegarDadosCatraca { get; set; }
    }

    public enum TipoAcaoPortariaEnum
    {
        [Display(Name ="Entrada 1")]
        Entrada_1 = 1,
        [Display(Name = "Saída 1")]
        Saida_1 = 2,
        [Display(Name = "Entrada 2")]
        Entrada_2 = 3,
        [Display(Name = "Saída 2")]
        Saida_2 = 4,
    }
}
