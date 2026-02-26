using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events;
using FinanceCore.Domain.Events.Goal;

namespace FinanceCore.Domain.Goals;

public class SavingsGoal : AggregateRoot
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public Money TargetAmount { get; private set; }
    public Money CurrentAmount { get; private set; }
    public DateTime? TargetDate { get; private set; }
    public EnGoalStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private void  Goal() { }

    public static SavingsGoal Create(
        Guid userId,
        string name,
        Money targetAmount,
        DateTime? targetDate = null,
        string? description = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Goal name cannot be empty.", nameof(name));

        if (targetAmount.Amount <= 0)
            throw new ArgumentException("Target amount must be positive.", nameof(targetAmount));

        var goal = new SavingsGoal
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = name.Trim(),
            Description = description?.Trim(),
            TargetAmount = targetAmount,
            CurrentAmount = new Money(0),
            TargetDate = targetDate,
            Status = EnGoalStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        goal.AddDomainEvent(new GoalCreatedEvent(
            goal.Id,
            goal.UserId,
            goal.Name,
            goal.TargetAmount));

        return goal;
    }

    public void AddContribution(Money amount)
    {
        if (Status != EnGoalStatus.Active)
            throw new InvalidOperationException("Cannot contribute to inactive goal.");

        if (amount.Amount <= 0)
            throw new ArgumentException("Contribution must be positive.", nameof(amount));

        CurrentAmount = CurrentAmount.Add(amount);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new GoalContributionAddedEvent(
            Id,
            amount,
            CurrentAmount,
            GetPercentageComplete()));

        // Check if goal completed
        if (CurrentAmount.Amount >= TargetAmount.Amount)
        {
            Complete();
        }
    }

    public void WithdrawContribution(Money amount)
    {
        if (Status == EnGoalStatus.Completed)
            throw new InvalidOperationException("Cannot withdraw from completed goal.");


        if (amount.Amount <= 0)
            throw new ArgumentException("Withdrawal must be positive.", nameof(amount));

        if (amount.Amount > CurrentAmount.Amount)
            throw new InvalidOperationException("Insufficient funds in goal.");

        CurrentAmount = CurrentAmount.Subtract(amount);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new GoalWithdrawalEvent(
            Id,
            amount,
            CurrentAmount));
    }

    private void Complete()
    {
        if (Status == EnGoalStatus.Completed)
            return;

        Status = EnGoalStatus.Completed;
        CompletedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new GoalCompletedEvent(
            Id,
            Name,
            TargetAmount,
            CurrentAmount));
    }

    public decimal GetPercentageComplete()
    {
        if (TargetAmount.Amount == 0) return 0;
        return Math.Min((CurrentAmount.Amount / TargetAmount.Amount) * 100, 100);
    }

    public Money GetRemainingAmount()
    {
        var remaining = TargetAmount.Amount - CurrentAmount.Amount;
        return new Money(remaining > 0 ? remaining : 0);
    }
}