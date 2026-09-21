namespace Domain.Dtos.Catracas;

public class CatracaEquipamentoDto
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Ip { get; set; } = string.Empty;
    public bool Online { get; set; }
    public override string ToString() => Descricao;
}

public class CatracaCartaoDto
{
    public int EquipamentoId { get; set; }
    public int Indice { get; set; }
    public int? PessoaId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string? Referencia1 { get; set; }
    public string? Referencia2 { get; set; }
    public bool Integrado { get; set; }
    public bool CadastradoSecullum { get; set; }
    public int? EstadoSecullum { get; set; }
    public string Empresa { get; set; } = string.Empty;
    public string Classificacao { get; set; } = string.Empty;
    public DateTime? DataCadastroSecullum { get; set; }
    public DateTime? DataAlteracaoSecullum { get; set; }
    public bool CadastradoCatraca { get; set; }
    public int QuantidadeBiometriasSecullum { get; set; }
    public int? QuantidadeBiometriasCatraca { get; set; }
    public bool BiometriaCatracaCarregada { get; set; }
    public bool Alterado { get; set; }
}

public class CatracaCartaoDetalheDto : CatracaCartaoDto
{
    public string Equipamento { get; set; } = string.Empty;
    public string Ip { get; set; } = string.Empty;
    public string TipoUsuario { get; set; } = string.Empty;
    public string NivelControle { get; set; } = string.Empty;
    public bool VerificarValidade { get; set; }
    public string? ValidadeInicial { get; set; }
    public string? ValidadeFinal { get; set; }
    public bool VerificarHorario { get; set; }
    public string Horarios { get; set; } = string.Empty;
    public List<int> Reles { get; set; } = [];
    public bool PossuiSenhaLiberacao { get; set; }
    public bool PossuiSenhaPanico { get; set; }
    public bool VerificarDigital { get; set; }
    public List<int> DedosPanico { get; set; } = [];
    public int QuantidadeBiometrias { get; set; }
}

public class SecullumPessoaIntegracaoDto
{
    public int Id { get; set; }
    public string NIdentificador { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public int? Estado { get; set; }
    public string Empresa { get; set; } = string.Empty;
    public string Classificacao { get; set; } = string.Empty;
    public DateTime? DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
}

public class SecullumClassificacaoDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
}

public class IntegrarCartaoRequestDto
{
    public int EquipamentoId { get; set; }
    public int Indice { get; set; }
}

public class EnviarUsuariosCatracaRequestDto
{
    public int EquipamentoId { get; set; }
    public List<int> PessoasIds { get; set; } = [];
    public List<int> IndicesRemover { get; set; } = [];
    public bool ExcluirSomenteIndicesInformados { get; set; }
}

public class AlterarUsuarioCatracaRequestDto
{
    public int EquipamentoId { get; set; }
    public int Indice { get; set; }
    public int PessoaId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string? Referencia1 { get; set; }
    public string? Referencia2 { get; set; }
    public string TipoUsuario { get; set; } = string.Empty;
    public string NivelControle { get; set; } = string.Empty;
    public bool VerificarDigital { get; set; }
}

public class EnvioUsuariosCatracaResultadoDto
{
    public int Enviados { get; set; }
    public int Removidos { get; set; }
    public List<string> Erros { get; set; } = [];
}

public class SincronizarCatracasRequestDto
{
    public int EquipamentoOrigemId { get; set; }
    public int EquipamentoDestinoId { get; set; }
    public List<int> PessoasIds { get; set; } = [];
    public bool RemoverAnteriores { get; set; }
}

public class SincronizarCatracasResultadoDto : EnvioUsuariosCatracaResultadoDto
{
}

public class CatracaBiometriaStatusDto
{
    public int EquipamentoId { get; set; }
    public int Indice { get; set; }
    public int? PessoaId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Empresa { get; set; } = string.Empty;
    public string Classificacao { get; set; } = string.Empty;
    public DateTime? DataCadastroSecullum { get; set; }
    public DateTime? DataAlteracaoSecullum { get; set; }
    public DateTime? UltimaEntrada { get; set; }
    public int QuantidadeCatraca { get; set; }
    public int QuantidadeSecullum { get; set; }
    public bool EnviadoPeloSecullum { get; set; }
    public bool PossuiNaCatraca { get; set; }
    public bool PossuiNoSecullum { get; set; }
}

public class ConsultarUltimasEntradasRequestDto
{
    public List<int> PessoasIds { get; set; } = [];
}

public class UltimaEntradaPessoaDto
{
    public int PessoaId { get; set; }
    public DateTime? UltimaEntrada { get; set; }
}

public class CatracaBiometriaSnapshotDto
{
    public int EquipamentoId { get; set; }
    public List<CatracaBiometriaStatusDto> Itens { get; set; } = [];
    public DateTime? AtualizadoEm { get; set; }
    public DateTime? AlteracaoSecullumSnapshot { get; set; }
    public bool SecullumAlterado { get; set; }
    public bool EmProcessamento { get; set; }
    public string? Erro { get; set; }
}

public class CatracaBiometriaNotificacaoDto
{
    public long Id { get; set; }
    public int EquipamentoId { get; set; }
    public string Equipamento { get; set; } = string.Empty;
    public DateTime ConcluidoEm { get; set; }
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; } = string.Empty;
}

public class RastreamentoCrachaDto
{
    public string NumeroPesquisado { get; set; } = string.Empty;
    public List<RastreamentoCrachaSecullumDto> Secullum { get; set; } = [];
    public List<RastreamentoCrachaSnapshotDto> Catracas { get; set; } = [];
    public List<string> SnapshotsNaoDisponiveis { get; set; } = [];
}

public class LiberacaoCrachaCatracaDto
{
    public string NumeroCracha { get; set; } = string.Empty;
    public int RegistrosCatracaExcluidos { get; set; }
    public int RegistrosSnapshotExcluidos { get; set; }
    public List<string> ErrosCatracas { get; set; } = [];
}

public class LiberacaoCrachaSecullumDto
{
    public string NumeroCracha { get; set; } = string.Empty;
    public int IdentificadoresLimpos { get; set; }
    public int ProvisoriosLimpos { get; set; }
    public int PeriodosProvisoriosLimpos { get; set; }
    public int NumerosVariadosExcluidos { get; set; }
}

public class RastreamentoCrachaSecullumDto
{
    public int PessoaId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CodigoPessoa { get; set; } = string.Empty;
    public int ClassificacaoId { get; set; }
    public string Classificacao { get; set; } = string.Empty;
    public string? NumeroCracha { get; set; }
    public string OrigemNumero { get; set; } = string.Empty;
    public int CodigoErro { get; set; }
    public DateTime? ProvisorioInicio { get; set; }
    public DateTime? ProvisorioFim { get; set; }
}

public class RastreamentoCrachaSnapshotDto
{
    public int EquipamentoId { get; set; }
    public int? PessoaId { get; set; }
    public string Equipamento { get; set; } = string.Empty;
    public string Ip { get; set; } = string.Empty;
    public int Indice { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string? Referencia1 { get; set; }
    public string? Referencia2 { get; set; }
    public string CampoEncontrado { get; set; } = string.Empty;
}
