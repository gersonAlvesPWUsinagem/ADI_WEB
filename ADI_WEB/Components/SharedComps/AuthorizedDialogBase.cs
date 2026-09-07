using Microsoft.AspNetCore.Components;
using MudBlazor;
using ADI_WEB.Security;

namespace ADI_WEB.Components.SharedComps;

public abstract class AuthorizedDialogBase : ComponentBase
{
    [CascadingParameter]
    protected IMudDialogInstance MudDialog { get; set; } = default!;

    [Inject]
    protected ICurrentUser CurrentUser { get; set; } = default!;

    [Inject]
    protected ISnackbar Snackbar { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var authorization = GetType().GetCustomAttributes(typeof(AuthorizeDialogAttribute), inherit: true)
            .OfType<AuthorizeDialogAttribute>()
            .SingleOrDefault();

        if (authorization is null)
        {
            return;
        }

        if (!authorization.Autoriza(CurrentUser))
        {
            Snackbar.Add(authorization.MensagemErro, Severity.Error);
            MudDialog.Cancel();
        }
    }
}
