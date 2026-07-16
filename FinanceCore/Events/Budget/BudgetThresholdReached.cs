using FinanceCore.Domain.Common;

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
