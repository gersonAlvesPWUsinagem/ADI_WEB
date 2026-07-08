using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class DadosToken
    {
        public string? Token { get; set; }
        public string? IssuedAt { get; set; }     // iat
        public string? Expires { get; set; }      // exp
        public string? Issuer { get; set; }       // iss
    }
}
