using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget threshold reached (e.g., 80% used)
    public record BudgetThresholdReachedEvent(
        Guid BudgetId,
        Guid CategoryId,
        Money BudgetAmount,
        Money SpentAmount,
        decimal PercentageUsed) : DomainEvent;
}
