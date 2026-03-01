using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Enums
{
    public enum EnGoalStatus : byte
    {
        Active = 1,
        Paused = 2,
        Completed = 3,
        Cancelled = 4
    }
}
