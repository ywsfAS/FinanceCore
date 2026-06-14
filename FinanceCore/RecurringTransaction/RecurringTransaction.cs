using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.Account;
using FinanceCore.Domain.Events.RecurringTransaction;
using System;

namespace FinanceCore.Domain.RecurringTransaction
{
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
        public bool IsActive { get; private set; }
        public DateTime? LastExecutedDate { get; private set; }

        public static RecurringTransaction Create(
            Guid accountId,
            Guid categoryId,
            Money amount,
            string description,
            EnTransactionType type,
            DateTime startDate,
            EnPeriod period,
            DateTime? endDate = null)
        {
            if (accountId == Guid.Empty)
                throw new ArgumentException("accountId is required");

            if (amount.IsLessOrEqual(Money.Zero(amount.Currency)))
                throw new ArgumentException("amount must be greater than zero");

            if (endDate.HasValue && endDate.Value < startDate)
                throw new ArgumentException("endDate cannot be before startDate");

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
                IsActive = true,
                LastExecutedDate = null
            };

            recurring.AddDomainEvent(new recurringTransactionCreatedEvent(recurring.Id, recurring.AccountId, recurring.Amount));
            return recurring;
        }

        public void UpdateDetails(
            Guid accountId,
            Guid categoryId,
            Money amount,
            string description,
            EnTransactionType type,
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
            Period = period;
            EndDate = endDate;

            AddDomainEvent(new RecurringTransactionUpdatedEvent(Id));
        }

        public void Activate()
        {
            IsActive = true;
            AddDomainEvent(new activateRecurringTransactionEvent(Id));
        }

        public void Deactivate()
        {
            IsActive = false;
            AddDomainEvent(new desactivateRecurringTransactionEvent(Id));
        }

        public void MarkAsExecuted(DateTime executionDate)
        {
            LastExecutedDate = executionDate;
            AddDomainEvent(new RecurringTransactionExecutedEvent(Id, AccountId, Amount, executionDate));
        }

        public DateTime GetNextExecutionDate(DateTime currentDate)
        {
            int k = CalculateCyclePassed(currentDate);
            if (k < 0) k = 0;
            return CalculateExpectedDate(k + 1);
        }

        public bool HasEnded(DateTime currentDate) =>
            EndDate.HasValue && currentDate > EndDate.Value;

        public bool CanExecute(DateTime currentDate)
        {
            if (!IsActive) return false;
            if (currentDate < StartDate) return false;
            if (EndDate.HasValue && currentDate > EndDate.Value) return false;

            int k = CalculateCyclePassed(currentDate);
            if (k < 0) return false;

            DateTime expectedDate = CalculateExpectedDate(k);
            DateTime lastExecuted = LastExecutedDate ?? DateTime.MinValue;

            if (expectedDate <= lastExecuted) return false;
            if (expectedDate > currentDate) return false;
            if (EndDate.HasValue && expectedDate > EndDate.Value) return false;

            return true;
        }

        private DateTime CalculateExpectedDate(int cycleNumber) => Period switch
        {
            EnPeriod.Daily => StartDate.AddDays(cycleNumber),
            EnPeriod.Weekly => StartDate.AddDays(cycleNumber * 7),
            EnPeriod.Monthly => StartDate.AddMonths(cycleNumber),
            _ => throw new ArgumentException("Invalid period")
        };

        private int CalculateCyclePassed(DateTime current) => Period switch
        {
            EnPeriod.Daily => (current.Date - StartDate.Date).Days,
            EnPeriod.Weekly => (current.Date - StartDate.Date).Days / 7,
            EnPeriod.Monthly => (current.Year - StartDate.Year) * 12 + (current.Month - StartDate.Month),
            _ => throw new ArgumentException("Invalid period")
        };
    }
}