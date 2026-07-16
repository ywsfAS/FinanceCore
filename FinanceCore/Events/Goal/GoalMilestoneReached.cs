using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Goal
{
    // Goal milestone reached (e.g., 25%, 50%, 75%)
    public record GoalMilestoneReachedEvent(
        Guid GoalId,
        string GoalName,
        decimal PercentageComplete,
        Money CurrentAmount,
        Money TargetAmount) : DomainEvent;
}
