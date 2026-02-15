using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget period changed
    public record BudgetPeriodChangedEvent(
        Guid BudgetId,
        BudgetPeriod OldPeriod,
        BudgetPeriod NewPeriod) : DomainEvent;
}
