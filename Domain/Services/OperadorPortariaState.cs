using Domain.Dtos.Portaria;

namespace Domain.Services;

public sealed class OperadorPortariaState
{
    public PorteiroOperadorDto? Operador { get; private set; }

    public void Definir(PorteiroOperadorDto operador)
    {
        Operador = operador;
    }

    public void Limpar()
    {
        Operador = null;
    }
}
