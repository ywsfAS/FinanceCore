using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal target date updated
    public record GoalTargetDateUpdatedEvent(
        Guid GoalId,
        DateTime? OldTargetDate,
        DateTime? NewTargetDate) : DomainEvent;
}
