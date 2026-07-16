using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget activated
    public record BudgetActivatedEvent(
        Guid BudgetId,
        Guid CategoryId) : DomainEvent;
}
