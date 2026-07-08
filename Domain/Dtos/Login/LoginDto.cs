using Domain.Helpers;
using Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Login
{  
    public class LoginDto
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "O ambiente é obrigatório.")]
        public EnvironmentEnum Environment { get; set; }
        public TargetOSTypeEnum SystemAccess { get; }

        [Required(ErrorMessage = "Não foi encontrado uma versão para o sistema")]
        public string? Version { get; set; }

        public LoginDto()
        {
            SystemAccess = SystemInfoHelper.GetOperatingSystem();
        }
    }
}
