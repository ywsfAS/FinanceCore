using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget reset (start of new period)
    public record BudgetResetEvent(
        Guid BudgetId,
        Guid CategoryId) : DomainEvent;
}
