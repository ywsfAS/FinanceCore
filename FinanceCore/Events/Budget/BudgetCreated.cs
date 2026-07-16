using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

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
