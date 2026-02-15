using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Budget
{
    // Spending added to budget (tracks each transaction)
    public record BudgetSpendingAddedEvent(
        Guid BudgetId,
        Guid CategoryId,
        Money Amount,
        Money TotalSpent,
        decimal PercentageUsed) : DomainEvent;
}
