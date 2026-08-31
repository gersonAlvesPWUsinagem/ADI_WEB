using MudBlazor;

namespace ADI_WEB.Components.SharedComps
{
    public class SnackbarComp
    {
        private readonly ISnackbar _snackbar;

        // Injeção por construtor
        public SnackbarComp(ISnackbar snackbar)
        {
            _snackbar = snackbar;
            // Configuração inicial
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomEnd;
        }

        public void Message(string message, Severity severity, Color color)
        {
            _snackbar.Add(message, severity, config =>
            {
                config.Icon = Icons.Material.Filled.CircleNotifications;
                config.IconColor = color;
                config.IconSize = Size.Large;
            });
        }
    }
}
