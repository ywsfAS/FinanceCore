using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Goal
{
    // Contribution added to goal
    public record GoalContributionAddedEvent(
        Guid ContributionId,
        Guid GoalId,
        Money ContributionAmount
        ) : DomainEvent;
}
