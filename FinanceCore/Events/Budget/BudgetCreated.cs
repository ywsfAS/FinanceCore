using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget created
    public record BudgetCreatedEvent(
        Guid BudgetId,
        Guid UserId,
        Guid CategoryId,
        Money Amount,
        EnPeriod Period) : DomainEvent;
}
