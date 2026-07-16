using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal resumed
    public record GoalResumedEvent(
        Guid GoalId,
        string Name) : DomainEvent;
}
