using Domain.Dtos.Catracas;
using Domain.Helpers;

namespace Domain.Interfaces;

public interface ICatracaService
{
    Task<ApiDataService<List<CatracaEquipamentoDto>>> ListarEquipamentosAsync();
    Task<ApiDataService<List<CatracaCartaoDto>>> ListarCartoesAsync(int equipamentoId, bool atualizar = false);
    Task<ApiDataService<CatracaCartaoDetalheDto>> ObterDetalheAsync(int equipamentoId, int indice);
    Task<ApiDataService<List<SecullumPessoaIntegracaoDto>>> ListarPessoasAsync();
    Task<ApiDataService<List<SecullumClassificacaoDto>>> ListarClassificacoesAsync();
    Task<ApiDataService<RastreamentoCrachaDto>> RastrearCrachaAsync(string numero);
    Task<ApiDataService<LiberacaoCrachaCatracaDto>> LiberarCrachaCatracaAsync(string numero);
    Task<ApiDataService<LiberacaoCrachaSecullumDto>> LiberarCrachaSecullumAsync(string numero);
    Task<ApiDataService<SecullumPessoaIntegracaoDto>> IntegrarAsync(IntegrarCartaoRequestDto dto);
    Task<ApiDataService<EnvioUsuariosCatracaResultadoDto>> EnviarUsuariosAsync(EnviarUsuariosCatracaRequestDto dto);
    Task<ApiDataService<List<CatracaBiometriaStatusDto>>> CarregarBiometriasAsync(int equipamentoId);
    Task<ApiDataService<CatracaBiometriaSnapshotDto>> ObterSnapshotBiometriaAsync(int equipamentoId);
    Task<ApiDataService<List<UltimaEntradaPessoaDto>>> ConsultarUltimasEntradasAsync(List<int> pessoasIds);
    Task<ApiDataService<bool>> IniciarCarregamentoBiometriasAsync(int equipamentoId);
    Task<ApiDataService<List<CatracaBiometriaNotificacaoDto>>> ListarNotificacoesBiometriaAsync(long depoisDe);
    Task<ApiDataService<bool>> AlterarUsuarioAsync(AlterarUsuarioCatracaRequestDto dto);
    Task<ApiDataService<List<CatracaCartaoDto>>> ListarSnapshotAsync(int equipamentoId);
    Task<ApiDataService<DateTime?>> ObterAtualizacaoSnapshotAsync(int equipamentoId);
    Task<ApiDataService<SincronizarCatracasResultadoDto>> SincronizarCatracasAsync(SincronizarCatracasRequestDto dto);
    Task<ApiDataService<CatracaComunicacaoProgressoDto>> ObterProgressoComunicacaoAsync(int equipamentoId);
}
