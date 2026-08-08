using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.RecurringTransaction;

namespace FinanceCore.Domain.RecurringTransaction;

public class RecurringTransaction : AggregateRoot
{
    public Guid AccountId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Money Amount { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public EnTransactionType Type { get; private set; }

    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public EnPeriod Period { get; private set; }
    public EnExecutionType ExecutionType { get; private set; }
    public EnRecurringTransactionStatus Status { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime? LastExecutedDate { get; private set; }
    public DateTime? NextExecutionAt { get; private set; }

    public RecurringTransaction() { }

    public static RecurringTransaction Create(
        Guid accountId,
        Guid categoryId,
        Money amount,
        string description,
        EnTransactionType type,
        EnExecutionType executionType,
        EnRecurringTransactionStatus status,
        DateTime startDate,
        EnPeriod period,
        DateTime? endDate = null)
    {
        if (accountId == Guid.Empty)
            throw new ArgumentException("AccountId is required.");

        if (amount.IsLessOrEqual(Money.Zero(amount.Currency)))
            throw new ArgumentException("Amount must be greater than zero.");

        if (endDate.HasValue && endDate.Value < startDate)
            throw new ArgumentException("EndDate cannot be before StartDate.");

        var recurring = new RecurringTransaction
        {
            Id = Guid.NewGuid(),
            AccountId = accountId,
            CategoryId = categoryId,
            Amount = amount,
            Description = description ?? string.Empty,
            Type = type,
            StartDate = startDate,
            EndDate = endDate,
            Period = period,
            ExecutionType = executionType,
            Status = status,
            IsActive = true,
            NextExecutionAt = startDate
        };

        recurring.AddDomainEvent(
            new recurringTransactionCreatedEvent(
                recurring.Id,
                recurring.AccountId,
                recurring.Amount));

        return recurring;
    }

    public void UpdateDetails(
        Guid accountId,
        Guid categoryId,
        Money amount,
        string description,
        EnTransactionType type,
        EnExecutionType executionType,
        DateTime startDate,
        EnPeriod period,
        DateTime? endDate)
    {
        AccountId = accountId;
        CategoryId = categoryId;
        Amount = amount;
        Description = description;
        Type = type;

        StartDate = startDate;
        EndDate = endDate;
        Period = period;

        ExecutionType = executionType;

        // Reset scheduling if it changes
        LastExecutedDate = null;
        NextExecutionAt = startDate;

        AddDomainEvent(new RecurringTransactionUpdatedEvent(Id));
    }

    public bool CanExecute(DateTime currentDate)
    {
        if (!IsActive)
            return false;

        if (Status is EnRecurringTransactionStatus.Paused or EnRecurringTransactionStatus.Completed)
            return false;

        if (!NextExecutionAt.HasValue)
            return false;

        return NextExecutionAt.Value <= currentDate;
    }

    public void MarkAsDue()
    {
        if (Status == EnRecurringTransactionStatus.Scheduled)
            Status = EnRecurringTransactionStatus.Due;
    }

    public void MarkAsScheduled()
    {
        Status = EnRecurringTransactionStatus.Scheduled;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void MarkAsExecuted(DateTime executionDate)
    {
        LastExecutedDate = executionDate;

        if (Period == EnPeriod.None)
        {
            NextExecutionAt = null;
            Status = EnRecurringTransactionStatus.Completed;
            IsActive = false;
        }
        else
        {
            NextExecutionAt = Period switch
            {
                EnPeriod.Daily => NextExecutionAt!.Value.AddDays(1),
                EnPeriod.Weekly => NextExecutionAt!.Value.AddDays(7),
                EnPeriod.Monthly => NextExecutionAt!.Value.AddMonths(1),
                _ => throw new InvalidOperationException($"Unsupported period '{Period}'.")
            };

            if (EndDate.HasValue && NextExecutionAt > EndDate)
            {
                NextExecutionAt = null;
                Status = EnRecurringTransactionStatus.Completed;
                IsActive = false;
            }
            else
            {
                Status = EnRecurringTransactionStatus.Scheduled;
            }
        }

        AddDomainEvent(
            new RecurringTransactionExecutedEvent(
                Id,
                AccountId,
                Amount,
                executionDate));
    }
}