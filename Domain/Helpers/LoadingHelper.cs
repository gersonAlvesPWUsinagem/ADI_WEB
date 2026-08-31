using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers;

public class LoadingHelper
{
    public event Action? OnChange;
    public bool IsLoading { get; private set; }

    public void Show()
    {
        IsLoading = true;
        NotifyStateChanged();
    }

    public void Hide()
    {
        IsLoading = false;
        NotifyStateChanged();
    }

    private void NotifyStateChanged()
    {
        // Garante que a notificação ocorra de forma segura, evitando erros de thread
        OnChange?.Invoke();
    }
}