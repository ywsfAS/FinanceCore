using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget period changed
    public record BudgetPeriodChangedEvent(
        Guid BudgetId,
        EnPeriod OldPeriod,
        EnPeriod NewPeriod) : DomainEvent;
}
