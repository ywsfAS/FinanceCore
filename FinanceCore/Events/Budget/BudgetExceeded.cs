using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget exceeded
    public record BudgetExceededEvent(
        Guid BudgetId,
        Guid CategoryId,
        Money BudgetAmount,
        Money SpentAmount) : DomainEvent;
}
