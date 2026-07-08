using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class ApiException : Exception
    {
        public int ErrorCode { get; }
        public bool Success { get; }

        // Mantemos o override para garantir que a propriedade Message 
        // da Exception base seja o que recebemos da API
        public override string Message { get; }

        // Construtor simples para o Front-end
        public ApiException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
            Message = message;
            Success = false;
        }

        // Caso queira manter compatibilidade se o código for compartilhado
        public ApiException(int errorCode) : base($"Erro {errorCode}")
        {
            ErrorCode = errorCode;
            Message = $"Erro código {errorCode}";
            Success = false;
        }
    }
}
