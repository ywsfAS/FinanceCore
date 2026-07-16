using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal created
    public record GoalCreatedEvent(
        Guid GoalId,
        Guid UserId,
        string Name,
        Money TargetAmount) : DomainEvent;
}
