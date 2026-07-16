using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget exceeded
    public record BudgetExceededEvent(
        Guid BudgetId,
        Guid CategoryId,
        Money BudgetAmount,
        Money SpentAmount) : DomainEvent;
}
