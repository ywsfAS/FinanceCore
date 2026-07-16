using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Goal
{
    public record SavingsGoalUpdatedEvent(Guid id):DomainEvent;
}
