using Domain.Helpers;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Login
{
    public class UserDto
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string? Password { get; set; }

        public EnvironmentEnum Environment { get; set; }

        public string? Plataforma { get; private set; }

        public TargetOSTypeEnum TargetOSType { get;  set; }

        public string? MacAddress { get; set; }

        public string? Version { get; set; }

        public int Empresa { get; set; }
        public int Filial { get; set; }
        public int Estabelecineto { get; set; }

        public string? IpV4 { get; set; }

        public string? Android { get; set; }

        public TargetOSTypeEnum TargetOSTypeWeb { get; set; }



        public UserDto()
        {
            Plataforma = "WEB";
        }
    }
}
