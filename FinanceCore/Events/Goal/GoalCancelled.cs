using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal cancelled
    public record GoalCancelledEvent(
        Guid GoalId,
        Guid UserId,
        string Name,
        Money CurrentAmount) : DomainEvent;
}
 