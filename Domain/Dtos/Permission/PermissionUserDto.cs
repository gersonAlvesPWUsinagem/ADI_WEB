using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Permission
{
    public class PermissionUserDto
    {
        public int UserPermission { get; set; }
        public ModuloEnum Modulo { get; set; }
        public int Permission { get; set; }
        public int Level { get; set; }
    }
}
