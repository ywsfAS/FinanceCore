using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget reset (start of new period)
    public record BudgetResetEvent(
        Guid BudgetId,
        Guid CategoryId) : DomainEvent;
}
