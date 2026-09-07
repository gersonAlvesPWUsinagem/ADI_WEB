using Domain.Dtos.Portaria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Login
{
    public sealed class OperadorLoginResultDto
    {
        public PorteiroOperadorDto? Operador { get; set; }
        public string? Senha { get; set; }
        public bool SolicitaAlteracaoSenha { get; set; }
    }
}
