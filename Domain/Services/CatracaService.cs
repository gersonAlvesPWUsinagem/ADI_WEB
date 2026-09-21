using Domain.Dtos.Catracas;
using Domain.Helpers;
using Domain.Interfaces;

namespace Domain.Services;

public class CatracaService : BaseHttpService, ICatracaService
{
    public CatracaService(HttpClient httpClient, IDataSessionHelper dataSession, IApiErrorContext errorContext)
        : base(httpClient, dataSession, errorContext) { }

    public Task<ApiDataService<List<CatracaEquipamentoDto>>> ListarEquipamentosAsync() =>
        GetAsync<List<CatracaEquipamentoDto>>("catracas/equipamentos");
    public Task<ApiDataService<List<CatracaCartaoDto>>> ListarCartoesAsync(int equipamentoId, bool atualizar = false) =>
        GetAsync<List<CatracaCartaoDto>>($"catracas/equipamentos/{equipamentoId}/cartoes?atualizar={atualizar.ToString().ToLowerInvariant()}");
    public Task<ApiDataService<CatracaCartaoDetalheDto>> ObterDetalheAsync(int equipamentoId, int indice) =>
        GetAsync<CatracaCartaoDetalheDto>($"catracas/equipamentos/{equipamentoId}/cartoes/{indice}");

    public Task<ApiDataService<CatracaComunicacaoProgressoDto>> ObterProgressoComunicacaoAsync(int equipamentoId) =>
        GetAsync<CatracaComunicacaoProgressoDto>($"catracas/equipamentos/{equipamentoId}/progresso-comunicacao");
    public Task<ApiDataService<List<SecullumPessoaIntegracaoDto>>> ListarPessoasAsync() =>
        GetAsync<List<SecullumPessoaIntegracaoDto>>("catracas/pessoas");
    public Task<ApiDataService<List<SecullumClassificacaoDto>>> ListarClassificacoesAsync() =>
        GetAsync<List<SecullumClassificacaoDto>>("catracas/classificacoes");
    public Task<ApiDataService<RastreamentoCrachaDto>> RastrearCrachaAsync(string numero) =>
        GetAsync<RastreamentoCrachaDto>($"catracas/rastreamento-cracha?numero={Uri.EscapeDataString(numero)}");
    public Task<ApiDataService<LiberacaoCrachaCatracaDto>> LiberarCrachaCatracaAsync(string numero) =>
        DeleteAsync<LiberacaoCrachaCatracaDto>($"catracas/rastreamento-cracha/{Uri.EscapeDataString(numero)}/catracas");
    public Task<ApiDataService<LiberacaoCrachaSecullumDto>> LiberarCrachaSecullumAsync(string numero) =>
        DeleteAsync<LiberacaoCrachaSecullumDto>($"catracas/rastreamento-cracha/{Uri.EscapeDataString(numero)}/secullum");
    public Task<ApiDataService<SecullumPessoaIntegracaoDto>> IntegrarAsync(IntegrarCartaoRequestDto dto) =>
        PostAsync<SecullumPessoaIntegracaoDto>("catracas/integracoes", dto);
    public Task<ApiDataService<EnvioUsuariosCatracaResultadoDto>> EnviarUsuariosAsync(EnviarUsuariosCatracaRequestDto dto) =>
        PostAsync<EnvioUsuariosCatracaResultadoDto>("catracas/envios", dto);
    public Task<ApiDataService<List<CatracaBiometriaStatusDto>>> CarregarBiometriasAsync(int equipamentoId) =>
        GetAsync<List<CatracaBiometriaStatusDto>>($"catracas/equipamentos/{equipamentoId}/biometrias");
    public Task<ApiDataService<CatracaBiometriaSnapshotDto>> ObterSnapshotBiometriaAsync(int equipamentoId) =>
        GetAsync<CatracaBiometriaSnapshotDto>($"catracas/equipamentos/{equipamentoId}/biometrias/snapshot");
    public Task<ApiDataService<List<UltimaEntradaPessoaDto>>> ConsultarUltimasEntradasAsync(List<int> pessoasIds) =>
        PostAsync<List<UltimaEntradaPessoaDto>>("catracas/biometrias/ultimas-entradas", new ConsultarUltimasEntradasRequestDto { PessoasIds = pessoasIds });
    public Task<ApiDataService<bool>> IniciarCarregamentoBiometriasAsync(int equipamentoId) =>
        PostAsync<bool>($"catracas/equipamentos/{equipamentoId}/biometrias/processamento", new { });
    public Task<ApiDataService<List<CatracaBiometriaNotificacaoDto>>> ListarNotificacoesBiometriaAsync(long depoisDe) =>
        GetAsync<List<CatracaBiometriaNotificacaoDto>>($"catracas/biometrias/notificacoes?depoisDe={depoisDe}");
    public Task<ApiDataService<bool>> AlterarUsuarioAsync(AlterarUsuarioCatracaRequestDto dto) =>
        PutAsync<bool>($"catracas/equipamentos/{dto.EquipamentoId}/cartoes/{dto.Indice}", dto);
    public Task<ApiDataService<List<CatracaCartaoDto>>> ListarSnapshotAsync(int equipamentoId) =>
        GetAsync<List<CatracaCartaoDto>>($"catracas/equipamentos/{equipamentoId}/snapshot");
    public Task<ApiDataService<DateTime?>> ObterAtualizacaoSnapshotAsync(int equipamentoId) =>
        GetAsync<DateTime?>($"catracas/equipamentos/{equipamentoId}/snapshot/atualizacao");
    public Task<ApiDataService<SincronizarCatracasResultadoDto>> SincronizarCatracasAsync(SincronizarCatracasRequestDto dto) =>
        PostAsync<SincronizarCatracasResultadoDto>("catracas/sincronizacoes", dto);
}
