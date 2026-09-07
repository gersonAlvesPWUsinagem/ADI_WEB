using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Portaria
{
    public class PorteiroOperadorDto
    {
        public int PoreteiroID { get; set; }
        public Guid Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Senha { get; set; }
        public bool ExigeAlteracaoSenha { get; set; }
        public bool SenhaProvisoriaExpirada { get; set; }
        public DateTime? DataLimiteAlteracaoSenha { get; set; }

    }
}
