using Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Dtos.Login
{
    public class DefSessionDtos
    {
        [Display(Name = "Cod Session")]
        public Guid Id { get; }

        [Required(ErrorMessage = "A Filial é obrigatória para a configuração da sessão.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um código de Filial válido.")]
        public int? Filial { get; set; }

        [Required(ErrorMessage = "O Estabelecimento é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um código de Estabelecimento válido.")]
        public int? Estabelecimento { get; set; }

        [Required(ErrorMessage = "A Empresa é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe um código de Empresa válido.")]
        public int? Empresa { get; set; }

        [Required(ErrorMessage = "O vínculo do Patrimônio da máquina local é obrigatório.")]
        public string Patrimonio { get; set; } = string.Empty;

        [Required(ErrorMessage = "O vínculo do IPV4 da máquina local é obrigatório.")]
        public string IPV4 { get; set; } = string.Empty;

        [Required(ErrorMessage = "O vínculo do MacAddress da máquina local é obrigatório.")]
        public string MacAddress { get; set; } = string.Empty;

        public EnvironmentEnum Environment { get; set; }


        public DefSessionDtos()
        {
            Id = Guid.NewGuid();
        }
    }
}