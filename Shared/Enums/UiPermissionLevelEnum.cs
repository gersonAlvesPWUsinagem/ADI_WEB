using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Enums
{
    [Flags]
    public enum UiPermissionLevelEnum
    {
        None = 0,
        MudAppBar = 1 << 0, // 1
        MudDrawer = 1 << 1, // 2
        BtInsert = 1 << 2, // 4
        BtUpdate = 1 << 3, // 8
        BtDelete = 1 << 4, // 16
        BtInsertCollection = 1 << 5, // 32
        BtUpdateCollection = 1 << 6, // 64
        BtDeleteCollection = 1 << 7, // 128

        // Atalho sênior: Full combina todas as permissões acima usando o operador OR (|)
        Full = MudAppBar | MudDrawer | BtInsert | BtUpdate | BtDelete | BtInsertCollection | BtUpdateCollection | BtDeleteCollection
    }
}
