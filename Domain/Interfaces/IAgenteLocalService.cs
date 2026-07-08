using Domain.Dtos.AgenteLocal;

namespace Domain.Interfaces
{
    public interface IAgenteLocalService
    {
        Task<AgenteLocalDto?> ObterDadosMaquinaAsync();
    }
}
