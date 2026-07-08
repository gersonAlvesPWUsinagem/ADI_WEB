using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Dtos.AgenteLocal
{
    public class AgenteLocalDto
    {
        [Display(Name = "Cod Session")]
        public Guid Id { get; }

        [JsonPropertyName("NomeMaquina")]
        public string? Patrimonio { get; set; } = string.Empty;

        [JsonPropertyName("IPAddress")]
        public string? IPV4 { get; set; } = string.Empty;

        [JsonPropertyName("MacAddress")]
        public string? MacAddress { get; set; } = string.Empty;

        public AgenteLocalDto()
        {
            Id = Guid.NewGuid();
        }
    }
}
