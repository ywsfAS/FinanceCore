using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Budget
{
    // Budget deactivated
    public record BudgetDeactivatedEvent(
        Guid BudgetId,
        Guid CategoryId) : DomainEvent;
}
