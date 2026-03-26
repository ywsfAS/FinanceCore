using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.Account;
using FinanceCore.Domain.Events.RecurringTransaction;
using System;

namespace FinanceCore.Domain.RecurringTransaction
{
    public class RecurringTransaction : AggregateRoot
    {
        public Guid accountId { get; private set; }
        public Guid categoryId { get; private set; }

        public decimal amount { get; private set; }
        public string description { get; private set; } = string.Empty;

        public EnTransactionType type { get; private set; }

        public DateTime startDate { get; private set; }
        public DateTime? endDate { get; private set; }

        public EnPeriod period { get; private set; }
        public int interval { get; private set; }
        public bool isActive { get; private set; }
        public DateTime? LastExecutedDate { get; private set; }

        public static RecurringTransaction Create(
            Guid accountId,
            Guid categoryId,
            decimal amount,
            string description,
            EnTransactionType type,
            DateTime startDate,
            EnPeriod period,
            int interval,
            DateTime? endDate = null
        )
        {
            if (accountId == Guid.Empty)
                throw new Exception("accountId is required");

            if (amount <= 0)
                throw new Exception("amount must be greater than zero");

            if (interval <= 0)
                throw new Exception("interval must be greater than zero");

            if (endDate.HasValue && endDate.Value < startDate)
                throw new Exception("endDate cannot be before startDate");

            var recurring = new RecurringTransaction
            {
                Id = Guid.NewGuid(),
                accountId = accountId,
                categoryId = categoryId,
                amount = amount,
                description = description ?? string.Empty,
                type = type,
                startDate = startDate,
                endDate = endDate,
                period = period,
                interval = interval,
                isActive = true,
                LastExecutedDate = null
            };

            recurring.AddDomainEvent(new recurringTransactionCreated(recurring.Id, recurring.accountId, recurring.amount));

            return recurring;
        }
        public void UpdateDetails(
            Guid accountId,
            Guid categoryId,
            decimal amount,
            string description,
            EnTransactionType type,
            DateTime startDate,
            EnPeriod period,
            int interval,
            DateTime? endDate)
        {
            this.accountId = accountId;
            this.categoryId = categoryId;
            this.amount = amount;
            this.description = description;
            this.type = type;
            this.startDate = startDate;
            this.period = period;
            this.interval = interval;
            this.endDate = endDate;

            // trigger domain event
            AddDomainEvent(new RecurringTransactionUpdatedEvent(this.Id));
        }
        private void _setIsActive(bool isActive)
        {
            this.isActive = isActive;
        }

        public void activate()
        {
            _setIsActive(true);
            AddDomainEvent(new activateRecurringTransaction(this.Id));
        }

        public void deactivate()
        {
            _setIsActive(false);
            AddDomainEvent(new desactivateRecurringTransaction(this.Id));
        }

        public void markAsExecuted(DateTime executionDate)
        {
            LastExecutedDate = executionDate;
            AddDomainEvent(new RecurringTransactionExecuted(this.Id, accountId, amount, executionDate));
        }

        public DateTime GetNextExecutionDate(DateTime currentDate)
        {
            int k = CalculateCyclePassedFrom(currentDate);
            if (k < 0) k = 0;
            return CalculateExpectedDate(k + 1);
        }

        public bool HasEnded(DateTime currentDate)
        {
            return endDate.HasValue && currentDate > endDate.Value;
        }

        private DateTime CalculateExpectedDate(int cycleNumber)
        {
            switch (period)
            {
                case EnPeriod.Daily:
                    return startDate.AddDays(cycleNumber * interval);

                case EnPeriod.Weekly:
                    return startDate.AddDays(cycleNumber * 7 * interval);

                case EnPeriod.Monthly:
                    return startDate.AddMonths(cycleNumber * interval);

                default:
                    throw new Exception("invalide period of recurring transaction");
            }
        }

        private int CalculateCyclePassedFrom(DateTime current)
        {
            switch (period)
            {
                case EnPeriod.Daily:
                    {
                        int days = (current.Date - startDate.Date).Days;
                        return days / interval;
                    }

                case EnPeriod.Weekly:
                    {
                        int days = (current.Date - startDate.Date).Days;
                        int weeks = days / 7;
                        return weeks / interval;
                    }

                case EnPeriod.Monthly:
                    {
                        int months = (current.Year - startDate.Year) * 12 + (current.Month - startDate.Month);
                        return months / interval;
                    }

                default:
                    throw new Exception("invalide period of recurring transaction");
            }
        }

        public bool CanExecute(DateTime currentDate)
        {
            if (!isActive)
                return false;

            if (currentDate < startDate)
                return false;

            if (endDate.HasValue && currentDate > endDate.Value)
                return false;

            int k = CalculateCyclePassedFrom(currentDate);

            if (k < 0)
                return false;

            DateTime expectedDate = CalculateExpectedDate(k);

            DateTime lastExecuted = LastExecutedDate ?? DateTime.MinValue;

            if (expectedDate <= lastExecuted)
                return false;

            if (expectedDate > currentDate)
                return false;

            if (endDate.HasValue && expectedDate > endDate.Value)
                return false;

            return true;
        }
    }
}