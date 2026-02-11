using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankArchiveMVP.Domain.Enums
{
    public enum OcrStatus
    {
        Pending = 0,
        Done = 1,
        Failed = 2,
        Manual = 3
    }
}
