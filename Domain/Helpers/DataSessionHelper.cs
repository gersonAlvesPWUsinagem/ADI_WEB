using Domain.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class DataSessionHelper: IDataSessionHelper
    {
        // Mantemos a instância interna
        public DataSession? DataSession { get; private set; }

        //private readonly IServiceProvider _serviceProvider;

        //public DataSessionService(IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;
        //}

        public async Task<string> LoadSession(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token)) return null!;

                DataSession = new DataSession();
                var clams = await TokenService.ExtractClaimsAsync(token);
                var clamsMetaData = await TokenService.GetTokenMetadataAsync(token);

                // Preenchimento manual para garantir que as referências da instância sejam mantidas
                DataSession.Matricula = clams.Matricula;
                DataSession.IdAdi = clams.IdAdi;
                DataSession.Login = clams.Login;
                DataSession.Name = clams.Name;
                DataSession.Level = clams.Level;
                DataSession.EnvironmentType = clams.Environment;
                DataSession.TargetOSType = clams.TargetOSType;

                DataSession.VersaoDoSistema = new VersaoDoSistema
                {
                    Plataforma = clams.Plataforma,
                    Android = clams.Android,
                    IpV4 = clams.IpV4,
                    MacAddress = clams.MacAddress,
                    VersaoAtual = clams.Version,
                };

                var version = await LoadVersion(DataSession.VersaoDoSistema);

                DataSession.VersaoDoSistema.NovaVersao = version.NovaVersao;
                DataSession.VersaoDoSistema.Quantidade = version.Quantidade;
                DataSession.VersaoDoSistema.IsObrigatoria = version.IsObrigatoria;

                DataSession.DadosToken = new DadosToken
                {
                    Token = token,
                    Expires = clamsMetaData.Expires,
                    IssuedAt = clamsMetaData.IssuedAt,
                    Issuer = clamsMetaData.Issuer
                };

                DataSession.Company = new DadosEmpresa
                {
                    Empresa = clams.Empresa,
                    Filial = clams.Filial,
                    Estabelecimento = clams.Estabelecimento,
                };

                // Salvando no SecureStorage
                string json = JsonSerializer.Serialize(DataSession);

                return json;
                //await SecureStorage.Default.SetAsync("user_session", json.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao carregar sessão: {ex.Message}");
            }
        }

        private async Task<VersaoDoSistema> LoadVersion(VersaoDoSistema param)
        {
            try
            {
                // Obtém o serviço de versão fora do construtor de forma segura
                //var versionService = _serviceProvider
                //    .GetRequiredService<IVersionService>();

                //var version = param.VersaoAtual.Split('.');
                //var major = int.Parse(version[0]);
                //var minor = int.Parse(version[1]);
                //var patch = int.Parse(version[2]);

                //var result = await versionService.
                //    GetValidarVersaoAtualAsync(3, major, minor, patch);

                //if (result.Value is not null)
                //{
                //    var data = new VersaoDoSistema
                //    {
                //        NovaVersao = string.Empty,
                //        Quantidade = 0,
                //        IsObrigatoria = false,
                //    };
                //    return data;
                //}

                await Task.CompletedTask;

                return new VersaoDoSistema();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao carregar sessão: {ex.Message}");
            }
        }

        public async Task<bool> RestoreSessionAsync(string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json)) return false;

                string tokenExtraido = string.Empty;

                // 1. TENTATIVA A: O JSON é o DataSession completo serializado?
                // (Isso acontece quando você recupera do SecureStorage)
                if (json.Contains("\"Matricula\"") || json.Contains("\"Login\""))
                {
                    var data = JsonSerializer.Deserialize<DataSession>(json);
                    if (data != null)
                    {
                        // Usa sua lógica de atribuição manual que você já criou
                        this.DataSession = new DataSession();
                        this.DataSession.Matricula = data.Matricula;
                        this.DataSession.Login = data.Login;
                        this.DataSession.Name = data.Name;
                        this.DataSession.Level = data.Level;
                        this.DataSession.EnvironmentType = data.EnvironmentType;
                        this.DataSession.TargetOSType = data.TargetOSType;
                        this.DataSession.VersaoDoSistema = data.VersaoDoSistema;
                        this.DataSession.DadosToken = data.DadosToken;
                        this.DataSession.Company = data.Company;
                        return true;
                    }
                }

                // 2. TENTATIVA B: O JSON é um "Envelope" de Navegação? 
                // (Isso acontece quando vem do Shell "{ token = eyJ... }")
                // Vamos extrair o valor bruto do token entre o "=" e o "}"
                if (json.Contains("token"))
                {
                    // Limpa a string para pegar apenas o que interessa (o JWT)
                    tokenExtraido = json.Split('=')
                                        .Last()
                                        .Replace("}", "")
                                        .Replace("\"", "")
                                        .Trim();

                    if (!string.IsNullOrEmpty(tokenExtraido))
                    {
                        // REUTILIZA seu método LoadSession que já faz o parse do JWT!
                        await LoadSession(tokenExtraido);
                        return DataSession != null;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro no Restore: {ex.Message}");
                await ClearAsync();
                return false;
            }
        }
        public async Task ClearAsync()
        {
            // 1. Limpa a Memória
            DataSession = null!;

            // 2. LIMPA O DISPOSITIVO (Crucial para o Logout funcionar)
            // SecureStorage.Default.Remove("user_session");

            await Task.CompletedTask;
        }
    }
}
