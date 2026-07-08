using System.ComponentModel;

namespace Shared.Enums;

public enum TipoRegistroEnum
{
    UNDEFINED = 0,  // Valor padrão para a classe base

    [Description("Colaborador")]
    COLABORADOR = 1,

    [Description("Crachá")]
    CRACHA = 2,

    [Description("Veículo")]
    VEICULO = 3,

    [Description("Chave")]
    REGISTRO_CHAVE = 4,

    [Description("Visitante")]
    REGISTRO_VISITANTE = 5,
}
