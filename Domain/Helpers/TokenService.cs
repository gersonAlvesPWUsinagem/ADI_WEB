using Shared.Enums;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public static class TokenService
    {
        // 1. EXTRAIR CLAIMS
        public static async Task<Employeer> ExtractClaimsAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null!;

            try
            {
                var handler = new JwtSecurityTokenHandler();

                if (!handler.CanReadToken(token))
                {
                    throw new Exception("Acesso Negado\nAs credenciais recebidas são inválidas. Tente fazer login novamente.");
                }

                var jwtToken = handler.ReadJwtToken(token);

                // --- Validações Numéricas (Enums e IDs) ---
                if (!int.TryParse(jwtToken.Claims.FirstOrDefault(c => c.Type == "Matricula")?.Value, out int matriculaInt))
                    throw new Exception("Não foi possível identificar seu número de matrícula.");

                if (!decimal.TryParse(jwtToken.Claims.FirstOrDefault(c => c.Type == "IdAdi")?.Value, out decimal idAdi))
                    throw new Exception("Não foi possível identificar seu número de registro no ADI.");

                if (!int.TryParse(jwtToken.Claims.FirstOrDefault(c => c.Type == "Environment")?.Value, out int environment))
                    throw new Exception("O ambiente de trabalho não foi identificado.");

                if (!int.TryParse(jwtToken.Claims.FirstOrDefault(c => c.Type == "TargetOSType")?.Value, out int targetOSType))
                    throw new Exception("Não foi possível validar o sistema operacional do dispositivo.");

                // --- Validações de Texto (Pattern Matching) ---
                if (jwtToken.Claims.FirstOrDefault(c => c.Type == "Login")?.Value is not string login || string.IsNullOrWhiteSpace(login))
                    throw new Exception("Seu nome de usuário (login) não foi encontrado.");

                if (jwtToken.Claims.FirstOrDefault(c => c.Type == "Name")?.Value is not string name || string.IsNullOrWhiteSpace(name))
                    throw new Exception("Seu nome completo não foi identificado.");

                if (jwtToken.Claims.FirstOrDefault(c => c.Type == "Level")?.Value is not string level || string.IsNullOrWhiteSpace(level))
                    throw new Exception("Seu nível de permissão não está definido.");

                if (jwtToken.Claims.FirstOrDefault(c => c.Type == "Plataforma")?.Value is not string plataforma || string.IsNullOrWhiteSpace(plataforma))
                    throw new Exception("A plataforma de acesso não foi reconhecida.");

                // --- Campos de Auditoria e Dispositivo (Adicionados agora) ---
                if (jwtToken.Claims.FirstOrDefault(c => c.Type == "Version")?.Value is not string version)
                    throw new Exception("Versão do aplicativo não identificada nos dados de acesso.");

                if (jwtToken.Claims.FirstOrDefault(c => c.Type == "MacAddress")?.Value is not string mac)
                    throw new Exception("Identificação física (MAC) do aparelho não encontrada.");

                if (jwtToken.Claims.FirstOrDefault(c => c.Type == "Android")?.Value is not string androidVersion)
                    throw new Exception("Versão do sistema Android não identificada.");

                if (jwtToken.Claims.FirstOrDefault(c => c.Type == "IpV4")?.Value is not string ip)
                    throw new Exception("Endereço de rede (IP) não identificado.");

                if (!int.TryParse(jwtToken.Claims.FirstOrDefault(c => c.Type == "Quantidade")?.Value, out int quantidade))
                    quantidade = 0;

                if (jwtToken.Claims.FirstOrDefault(c => c.Type == "NovaVersao")?.Value is not string novaVersao)
                    novaVersao = string.Empty;

                bool isObrigatoro = false;

                if ((jwtToken.Claims.FirstOrDefault(c => c.Type == "IsObrigatoro")?.Value == "1"))
                    isObrigatoro = true;

                if (!int.TryParse(jwtToken.Claims.FirstOrDefault(c => c.Type == "Empresa")?.Value, out int empresa))
                    throw new Exception("Não foi possível identificar dados da empresa.");

                if (!int.TryParse(jwtToken.Claims.FirstOrDefault(c => c.Type == "Filial")?.Value, out int filial))
                    throw new Exception("Não foi possível identificar dados da empresa.");

                if (!int.TryParse(jwtToken.Claims.FirstOrDefault(c => c.Type == "Estabelecimento")?.Value, out int estabelecimento))
                    throw new Exception("Não foi possível identificar dados da empresa.");

                await Task.CompletedTask;

                return new Employeer
                {
                    Matricula = matriculaInt,
                    IdAdi = idAdi,
                    Login = login,
                    Name = name,
                    Level = level,
                    Environment = (EnvironmentEnum)environment,
                    Plataforma = plataforma,
                    TargetOSType = (TargetOSTypeEnum)targetOSType,
                    Version = version,
                    MacAddress = mac,
                    Android = androidVersion,
                    IpV4 = ip,
                    NovaVersao = novaVersao,
                    Quantidade = quantidade,
                    IsObrigatoro = isObrigatoro,
                    Empresa = empresa,
                    Filial = filial,
                    Estabelecimento = estabelecimento
                };
            }
            catch (Exception ex)
            {
                // Relança a exceção com a mensagem amigável para ser capturada pela ViewModel
                throw new Exception($"Perfil Incompleto\n{ex.Message}\n\nEntre em contato com o suporte técnico.");
            }
        }

        // 2. VALIDAR TOKEN (recebe o token)
        public static bool IsTokenValid(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            return ValidateTokenExpiration(token);
        }

        // 3. VALIDAR EXPIRAÇÃO
        private static bool ValidateTokenExpiration(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var expClaim = jwt.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

                if (!long.TryParse(expClaim, out var expUnix))
                    return false;

                var expiryDate = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;

                // --- AJUSTE AQUI ---
                // Para TESTE (Token de 1 min): Use 5 ou 10 segundos de margem
                return expiryDate > DateTime.UtcNow.AddSeconds(10);

                // Para PRODUÇÃO (Token de 4 horas): O seu código original com 5 minutos é ótimo
                // return expiryDate > DateTime.UtcNow.AddMinutes(5);
            }
            catch
            {
                return false;
            }
        }
        public async static Task<DadosToken> GetTokenMetadataAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null!;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Função interna para converter Unix Timestamp em String formatada
                string ToReadableDate(string unixValue)
                {
                    if (long.TryParse(unixValue, out long unixTime))
                    {
                        return DateTimeOffset.FromUnixTimeSeconds(unixTime)
                            .ToLocalTime()
                            .ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    return "N/A";
                }
                await Task.CompletedTask;

                return new DadosToken
                {
                    Token = token,
                    Issuer = jwtToken.Issuer ?? "Não Identificado",
                    // Convertendo iat (emissão) e exp (expiração) para formato legível
                    IssuedAt = ToReadableDate(jwtToken.Claims.FirstOrDefault(c => c.Type == "iat")?.Value ?? throw new Exception("Erro fatal!")),
                    Expires = ToReadableDate(jwtToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value ?? throw new Exception("Erro fatal!"))
                };
            }
            catch
            {
                return null!;
            }
        }
        // 4. DEBUG OPCIONAL
        public static string DecodeTokenForDebug(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (!handler.CanReadToken(token))
                    return "Token inválido";

                var jwtToken = handler.ReadJwtToken(token);

                var sb = new StringBuilder();
                sb.AppendLine("=== TOKEN DECODIFICADO ===");

                foreach (var claim in jwtToken.Claims)
                    sb.AppendLine($"{claim.Type}: {claim.Value}");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return $"Erro: {ex.Message}";
            }
        }
    }

}
