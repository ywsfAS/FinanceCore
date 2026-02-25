using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.Budget;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Budgets
{

    public class Budget : AggregateRoot
    {
        public Guid UserId { get; private set; }
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public EnCurrency Currency { get; private set; }
        public Money Amount { get; private set; }
        public BudgetPeriod Period { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Budget() { }

        private Budget(
            Guid budgetId,
            Guid userId,
            Guid categoryId,
            string name,
            EnCurrency currency,
            Money amount,
            BudgetPeriod period,
            DateTime startDate,
            DateTime endDate,
            DateTime createdAt,
            DateTime? updatedAt)
        {
            Id = budgetId;
            UserId = userId;
            CategoryId = categoryId;
            Name = name;
            Currency = currency;
            Amount = amount;
            Period = period;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        // Reconstitute from persistence
        public static Budget Create(
            Guid budgetId,
            Guid userId,
            Guid categoryId,
            string name,
            EnCurrency currency,
            decimal amount,
            BudgetPeriod period,
            DateTime startDate,
            DateTime endDate,
            DateTime createdAt,
            DateTime? updatedAt = null)
        {
            return new Budget(
                budgetId, userId, categoryId, name, currency,
                new Money(amount), period, startDate, endDate, createdAt, updatedAt);
        }

        // Create new budget
        public static Budget Create(
            Guid userId,
            Guid categoryId,
            string name,
            EnCurrency currency,
            decimal amount,
            BudgetPeriod period,
            DateTime startDate)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.", nameof(userId));

            if (categoryId == Guid.Empty)
                throw new ArgumentException("Category ID cannot be empty.", nameof(categoryId));

            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidBudgetNameException(name, "Budget name cannot be empty");

            if (name.Length > 100)
                throw new InvalidBudgetNameException(name, "Budget name cannot exceed 100 characters");

            if (name.Length < 2)
                throw new InvalidBudgetNameException(name, "Budget name must be at least 2 characters");

            if (amount <= 0)
                throw new InvalidBudgetAmountException(amount);

            if (!Enum.IsDefined(typeof(BudgetPeriod), period))
                throw new InvalidBudgetPeriodException(period);

            if (startDate < DateTime.UtcNow.AddYears(-1))
                throw new InvalidBudgetDateRangeException(startDate, startDate,
                    "Start date cannot be more than 1 year in the past");

            var endDate = CalculateEndDate(startDate, period);

            var budget = new Budget
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CategoryId = categoryId,
                Name = name.Trim(),
                Currency = currency,
                Amount = new Money(amount),
                Period = period,
                StartDate = startDate,
                EndDate = endDate,
                CreatedAt = DateTime.UtcNow
            };

            budget.AddDomainEvent(new BudgetCreatedEvent(
                budget.Id,
                budget.UserId,
                budget.CategoryId,
                budget.Amount.Amount,
                budget.Period));

            return budget;
        }

        public void UpdateAmount(Money newAmount)
        {
            if (newAmount.Amount <= 0)
                throw new InvalidBudgetAmountException(newAmount.Amount);

            var oldAmount = Amount;
            Amount = newAmount;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new BudgetAmountUpdatedEvent(Id, oldAmount.Amount, newAmount.Amount));
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new InvalidBudgetNameException(newName, "Budget name cannot be empty");

            if (newName.Length > 100)
                throw new InvalidBudgetNameException(newName, "Budget name cannot exceed 100 characters");

            if (newName.Length < 2)
                throw new InvalidBudgetNameException(newName, "Budget name must be at least 2 characters");

            if (newName.Trim() == Name)
                return;

            Name = newName.Trim();
            UpdatedAt = DateTime.UtcNow;
        }

        public void ExtendPeriod(BudgetPeriod newPeriod)
        {
            if (!Enum.IsDefined(typeof(BudgetPeriod), newPeriod))
                throw new InvalidBudgetPeriodException(newPeriod);

            var oldPeriod = Period;
            Period = newPeriod;
            EndDate = CalculateEndDate(StartDate, newPeriod);
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new BudgetPeriodChangedEvent(Id, oldPeriod, newPeriod));
        }

        public bool IsPeriodExpired() => DateTime.UtcNow > EndDate;

        public bool IsPeriodActive() => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;

        public int GetDaysRemaining()
        {
            if (IsPeriodExpired()) return 0;
            return (EndDate - DateTime.UtcNow).Days;
        }

        private static DateTime CalculateEndDate(DateTime startDate, BudgetPeriod period)
        {
            return period switch
            {
                BudgetPeriod.Weekly => startDate.AddDays(7).AddSeconds(-1),
                BudgetPeriod.Monthly => startDate.AddMonths(1).AddSeconds(-1),
                BudgetPeriod.Quarterly => startDate.AddMonths(3).AddSeconds(-1),
                BudgetPeriod.Yearly => startDate.AddYears(1).AddSeconds(-1),
                _ => throw new InvalidBudgetPeriodException(period)
            };
        }
    }

}
