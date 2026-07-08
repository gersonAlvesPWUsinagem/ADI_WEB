using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Login
{
    public class LoginResponseDto
    {
        public string? Token { get; set; }
        public bool Success { get; set; }
    }
}
