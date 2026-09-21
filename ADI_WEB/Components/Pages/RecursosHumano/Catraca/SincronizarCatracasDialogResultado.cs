namespace ADI_WEB.Components.Pages.RecursosHumano.Catraca;

public sealed record SincronizarCatracasDialogResultado(
    int EquipamentoDestinoId,
    string TipoCopia,
    bool RemoverAnteriores);
