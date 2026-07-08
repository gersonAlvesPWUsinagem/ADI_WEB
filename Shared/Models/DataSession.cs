using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class DataSession
    {
        public Guid? Id { get; private set; }
        public decimal IdAdi { get; set; }
        public int Matricula { get; set; }
        public string? Login { get; set; }
        public string? Name { get; set; }
        public string? Level { get; set; }
        public EnvironmentEnum? EnvironmentType { get; set; }
        public TargetOSTypeEnum? TargetOSType { get; set; }
        public VersaoDoSistema? VersaoDoSistema { get; set; }
        public DadosToken DadosToken { get; set; } = new();
        public DadosEmpresa Company { get; set; } = new();
        public DataSession()
        {
            // Gera um Guid único automaticamente ao instanciar
            Id = Guid.NewGuid();
        }
    }
}