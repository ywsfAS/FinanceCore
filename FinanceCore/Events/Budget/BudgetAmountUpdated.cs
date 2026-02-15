using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget amount updated
    public record BudgetAmountUpdatedEvent(
        Guid BudgetId,
        Money OldAmount,
        Money NewAmount) : DomainEvent;
}
