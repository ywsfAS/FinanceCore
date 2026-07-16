using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget amount updated
    public record BudgetAmountUpdatedEvent(
        Guid BudgetId,
        Money OldAmount,
        Money NewAmount) : DomainEvent;
}
