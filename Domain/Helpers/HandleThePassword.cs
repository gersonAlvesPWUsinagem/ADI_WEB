using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    /// <summary>
    /// Classe estática responsável por manipular senhas, incluindo criptografia e verificação.
    /// </summary>
    public static class HandleThePassword
    {
        /// <summary>
        /// Encripta a senha fornecida usando um algoritmo simples de transformação de caracteres.
        /// </summary>
        /// <param name="cSenha">A senha a ser encriptada.</param>
        /// <returns>Retorna a senha encriptada como uma string.</returns>
        public static string EncriptarSenha(string cSenha)
        {
            cSenha = cSenha.Trim();
            int nLen = cSenha.Length;
            int nFator = 1;
            string cMensa = "";

            for (int nCont = 1; nCont <= nLen; nCont++)
            {
                int nV = (int)cSenha[nCont - 1]; // Pega o valor ASCII do caractere atual
                nV = nV * nFator + 1;
                nV = nV + nCont;

                if (nFator > 2)
                {
                    nFator = 1;
                }

                cMensa += (char)nV; // Converte o valor de volta para caractere e adiciona à string final
            }

            return cMensa;
        }

        /// <summary>
        /// Criptografa o texto fornecido invertendo os valores ASCII dos caracteres.
        /// </summary>
        /// <param name="text">O texto a ser criptografado.</param>
        /// <returns>Retorna o texto criptografado como uma string.</returns>
        /// <exception cref="ArgumentException">Lançada se o texto for nulo ou vazio.</exception>
        public static string Crypt(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("O texto não pode ser nulo ou vazio.", nameof(text));

            char[] characters = text.ToCharArray();

            for (int i = 0; i < characters.Length; i++)
            {
                int asciiValue = (int)characters[i];

                if (asciiValue < 128)
                {
                    asciiValue += 128;
                }
                else if (asciiValue > 128)
                {
                    asciiValue -= 128;
                }

                characters[i] = (char)asciiValue;
            }

            return new string(characters);
        }

        /// <summary>
        /// Verifica se a senha fornecida está criptografada.
        /// </summary>
        /// <param name="senha">A senha a ser verificada.</param>
        /// <returns>Retorna true se a senha estiver criptografada; caso contrário, false.</returns>
        public static bool IsCripted(string senha)
        {
            // Verifica se todos os caracteres estão dentro de um intervalo legível (ex: ASCII 32-126)
            // Senhas criptografadas geralmente terão caracteres fora deste intervalo
            return senha.Any(c => (int)c < 32 || (int)c > 126);
        }
    }
}
