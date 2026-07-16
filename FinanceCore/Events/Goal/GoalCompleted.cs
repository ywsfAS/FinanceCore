using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal completed
    public record GoalCompletedEvent(
        Guid GoalId,
        string Name,
        Money TargetAmount,
        Money FinalAmount) : DomainEvent;
}
