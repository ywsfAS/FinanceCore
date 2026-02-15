using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget deleted
    public record BudgetDeletedEvent(
        Guid BudgetId,
        Guid UserId,
        Guid CategoryId) : DomainEvent;
}
