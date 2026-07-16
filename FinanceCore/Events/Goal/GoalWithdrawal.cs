using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.Goal
{
    // Withdrawal from goal
    public record GoalWithdrawalEvent(
        Guid GoalId,
        Money WithdrawalAmount,
        Money RemainingAmount) : DomainEvent;
}
