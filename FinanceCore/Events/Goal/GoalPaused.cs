using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal paused
    public record GoalPausedEvent(
        Guid GoalId,
        string Name) : DomainEvent;
}
