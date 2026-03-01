using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Enums
{
    public enum EnAccountType : byte
    {
        Checking = 1,
        Savings = 2,
        Credit = 3,
        Cash = 4,
        Investment = 5,
        Loan = 6,
        Other = 99
    }
}
