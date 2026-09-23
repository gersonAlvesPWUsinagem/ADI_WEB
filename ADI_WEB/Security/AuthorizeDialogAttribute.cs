namespace ADI_WEB.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class AuthorizeDialogAttribute : Attribute
{

    public AuthorizeDialogAttribute(params int[] niveisPermitidos)
    {
        NiveisPermitidos = niveisPermitidos ?? Array.Empty<int>();
    }

    public IReadOnlyList<int> NiveisPermitidos { get; }

    public string MensagemErro { get; set; } = "Você não possui permissão para abrir este diálogo.";

    public bool Autoriza(ICurrentUser usuario)
    {
        if (usuario is null || !usuario.IsAuthenticated)
        {
            return false;
        }

        return NiveisPermitidos.Any(nivelPermitido =>
            PermissionLevelPolicy.Allows(usuario.Level, nivelPermitido));
    }
}
