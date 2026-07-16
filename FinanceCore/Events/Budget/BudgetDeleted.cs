using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget deleted
    public record BudgetDeletedEvent(
        Guid BudgetId,
        Guid UserId,
        Guid CategoryId) : DomainEvent;
}
