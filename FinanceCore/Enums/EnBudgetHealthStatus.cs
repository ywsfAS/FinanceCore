using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Enums
{
    public enum EnBudgetHealthStatus : byte
    {
        Healthy = 0,
        Warning = 1,
        OverBudget = 2,
        Critical = 3,
        Unknown = 4,


    }
}
