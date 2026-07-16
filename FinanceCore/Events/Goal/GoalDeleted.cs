using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Goal
{
    public record GoalDeletedEvent(
        Guid GoalId,
        Guid UserId,
        string Name) : DomainEvent;
}
