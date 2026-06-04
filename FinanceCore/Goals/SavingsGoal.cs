using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events;
using FinanceCore.Domain.Events.Goal;
using FinanceCore.Domain.Exceptions;

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

    private SavingsGoal() { }

    public static SavingsGoal Create(
        Guid? Id,
        Guid userId,
        string name,
        Money targetAmount,
        DateTime? targetDate = null,
        string? description = null)
    {
        if (userId == Guid.Empty)
            throw new UserIdNotProvidedException();

        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidGoalName(name);

        if (targetAmount.IsLessOrEqual(Money.Zero(targetAmount.Currency)))
            throw new InvalidGoalTarget(targetAmount);

        var goal = new SavingsGoal
        {
            Id = Id ?? Guid.NewGuid(),
            UserId = userId,
            Name = name.Trim(),
            Description = description?.Trim(),
            TargetAmount = targetAmount,
            CurrentAmount = Money.Zero(targetAmount.Currency),
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
    public void UpdateDetails(string name, Money targetAmount, DateTime? targetDate, string? description)
    {

        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidGoalName(name);

        if (targetAmount.IsLessOrEqual(Money.Zero(targetAmount.Currency)))
            throw new InvalidGoalTarget(targetAmount);

        if (targetAmount.IsLessThan(CurrentAmount))
            throw new GoalTargetBelowCurrentAmountException(targetAmount,CurrentAmount);
        if (targetDate.HasValue &&
        targetDate.Value <= DateTime.UtcNow)
        {
            throw new InvalidGoalTargetDateException();
        }

        Name = name;
        TargetAmount = targetAmount;
        TargetDate = targetDate;
        Description = description;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new SavingsGoalUpdatedEvent(Id));
    }
    private bool IsCompleted()
    {
        return CurrentAmount.IsGreaterOrEqual(TargetAmount);
    }
    public void AddContribution(Money amount)
    {
        if (Status != EnGoalStatus.Active)
            throw new InvalidOperationException("Cannot contribute to inactive goal.");

        if (amount.IsLessOrEqual(Money.Zero(amount.Currency)))
            throw new InvalidContributionAmountException(amount.Amount);

        CurrentAmount = CurrentAmount.Add(amount);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new GoalContributionAddedEvent(
            Id,
            amount,
            CurrentAmount,
            GetPercentageComplete()));

        // Check if goal completed
        if (IsCompleted())
        {
            Complete();
        }
    }

    public void WithdrawContribution(Money amount)
    {
        if (Status == EnGoalStatus.Completed)
            throw new CannotWithdrawFromCompletedGoalException(Id); ;


        if (amount.IsLessOrEqual(Money.Zero(amount.Currency)))
            throw new InvalidContributionAmountException(amount.Amount);

        if (amount.IsGreaterOrEqual(CurrentAmount))
            throw new InsufficientGoalFundsException(Id, amount.Amount, CurrentAmount.Amount);
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
    public void Pause()
    {
        if (Status != EnGoalStatus.Active) return;
        Status = EnGoalStatus.Paused;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new GoalPausedEvent(Id,Name));
    }
    public void Resume()
    {
        if (Status != EnGoalStatus.Paused) return;
        Status = EnGoalStatus.Active;
        UpdatedAt = DateTime.UtcNow;
        AddDomainEvent(new GoalResumedEvent(Id,Name));
    }
    public void Cancel()
    {
        if (Status == EnGoalStatus.Completed)
            throw new CannotCancelCompletedGoalException(Id);

        Status = EnGoalStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new GoalCancelledEvent(Id,UserId,Name,CurrentAmount));
    }
    public decimal GetPercentageComplete()
    {
        if (TargetAmount.Amount == 0) return 0;
        return Math.Min((CurrentAmount.Amount / TargetAmount.Amount) * 100, 100);
    }
    public Money GetRemainingAmount()
    {
        if (CurrentAmount.IsGreaterOrEqual(TargetAmount))
            return Money.Zero(CurrentAmount.Currency);

        return TargetAmount.Subtract(CurrentAmount);
    }
}