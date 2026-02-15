using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Enums
{
    public enum EnTransactionStatus
    {
        Completed = 1,    // Successfully processed
        Voided = 2        // Cancelled/reversed
    }
}
