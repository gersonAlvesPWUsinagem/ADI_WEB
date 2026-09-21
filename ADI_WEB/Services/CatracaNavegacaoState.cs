namespace ADI_WEB.Services;

public sealed class CatracaNavegacaoState
{
    private CatracaRetornoDetalhe? _retorno;

    public void RegistrarIdaParaDetalhe(int equipamentoId, bool catracaCarregada) =>
        _retorno = new(equipamentoId, catracaCarregada);

    public CatracaRetornoDetalhe? ConsumirRetorno()
    {
        var retorno = _retorno;
        _retorno = null;
        return retorno;
    }
}

public sealed record CatracaRetornoDetalhe(int EquipamentoId, bool CatracaCarregada);
