using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankArchiveMVP.Domain.Enums
{
    public enum DocumentStatus
    {
        New = 0,
        Checked = 1,
        Archived = 2,
        NeedsCorrection = 3
    }
}
