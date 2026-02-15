using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.Budget;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Budgets;

public class Budget : AggregateRoot
{
    public Guid UserId { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Money Amount { get; private set; }
    public Money SpentAmount { get; private set; }
    public BudgetPeriod Period { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Budget() { }

    public static Budget Create(
        Guid userId,
        Guid categoryId,
        string name,
        Money amount,
        BudgetPeriod period,
        DateTime startDate)
    {
        // Validate user ID
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));

        // Validate category ID
        if (categoryId == Guid.Empty)
            throw new ArgumentException("Category ID cannot be empty.", nameof(categoryId));

        // Validate name
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidBudgetNameException(name, "Budget name cannot be empty");

        if (name.Length > 100)
            throw new InvalidBudgetNameException(name, "Budget name cannot exceed 100 characters");

        if (name.Length < 2)
            throw new InvalidBudgetNameException(name, "Budget name must be at least 2 characters");

        // Validate amount
        if (amount.Amount <= 0)
            throw new InvalidBudgetAmountException(amount.Amount);

        // Validate period
        if (!Enum.IsDefined(typeof(BudgetPeriod), period))
            throw new InvalidBudgetPeriodException(period);

        // Validate start date (optional: prevent dates too far in past)
        if (startDate < DateTime.UtcNow.AddYears(-1))
            throw new InvalidBudgetDateRangeException(
                startDate,
                startDate,
                "Start date cannot be more than 1 year in the past");

        var endDate = CalculateEndDate(startDate, period);

        var budget = new Budget
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CategoryId = categoryId,
            Name = name.Trim(),
            Amount = amount,
            SpentAmount = new Money(0, amount.Currency),
            Period = period,
            StartDate = startDate,
            EndDate = endDate,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        budget.AddDomainEvent(new BudgetCreatedEvent(
            budget.Id,
            budget.UserId,
            budget.CategoryId,
            budget.Amount,
            budget.Period));

        return budget;
    }

    public void AddSpending(Money amount)
    {
        // Check if active
        if (!IsActive)
            throw new InactiveBudgetException(Id, Name, "add spending");

        // Check if period expired
        if (IsPeriodExpired())
            throw new BudgetPeriodExpiredException(Id, EndDate);

        // Check currency match
        if (amount.Currency != SpentAmount.Currency)
            throw new BudgetCurrencyMismatchException(Id, SpentAmount.Currency, amount.Currency);

        // Validate amount
        if (amount.Amount <= 0)
            throw new InvalidSpendingAmountException(amount.Amount);

        var previousSpent = SpentAmount;
        SpentAmount = SpentAmount.Add(amount);
        UpdatedAt = DateTime.UtcNow;

        // Add event for spending added
        AddDomainEvent(new BudgetSpendingAddedEvent(
            Id,
            CategoryId,
            amount,
            SpentAmount,
            GetPercentageUsed()));

        // Check if budget exceeded
        if (IsExceeded() && !WasExceeded(previousSpent))
        {
            AddDomainEvent(new BudgetExceededEvent(
                Id,
                CategoryId,
                Amount,
                SpentAmount));
        }

        // Check if approaching limit (e.g., 80%)
        var currentPercentage = GetPercentageUsed();
        var previousPercentage = GetPercentageUsed(previousSpent);

        if (currentPercentage >= 80 && previousPercentage < 80)
        {
            AddDomainEvent(new BudgetThresholdReachedEvent(
                Id,
                CategoryId,
                Amount,
                SpentAmount,
                currentPercentage));
        }
    }

    public void UpdateAmount(Money newAmount)
    {
        // Check if active
        if (!IsActive)
            throw new InactiveBudgetException(Id, Name, "update amount");

        // Validate amount
        if (newAmount.Amount <= 0)
            throw new InvalidBudgetAmountException(newAmount.Amount);

        // Check currency match
        if (newAmount.Currency != Amount.Currency)
            throw new BudgetCurrencyChangeException(Id, Amount.Currency, newAmount.Currency);

        // Validate new amount is not less than spent (optional business rule)
        // Comment out if you want to allow setting budget below spent amount
        if (newAmount.Amount < SpentAmount.Amount)
            throw new BudgetAmountBelowSpentException(Id, newAmount, SpentAmount);

        var oldAmount = Amount;
        Amount = newAmount;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new BudgetAmountUpdatedEvent(
            Id,
            oldAmount,
            newAmount));
    }

    public void UpdateName(string newName)
    {
        // Check if active
        if (!IsActive)
            throw new InactiveBudgetException(Id, Name, "update name");

        // Validate name
        if (string.IsNullOrWhiteSpace(newName))
            throw new InvalidBudgetNameException(newName, "Budget name cannot be empty");

        if (newName.Length > 100)
            throw new InvalidBudgetNameException(newName, "Budget name cannot exceed 100 characters");

        if (newName.Length < 2)
            throw new InvalidBudgetNameException(newName, "Budget name must be at least 2 characters");

        if (newName.Trim() == Name)
            return; // No change

        Name = newName.Trim();
        UpdatedAt = DateTime.UtcNow;
    }

    public void Reset()
    {
        // Check if active
        if (!IsActive)
            throw new InactiveBudgetException(Id, Name, "reset");
        SpentAmount = new Money(0, Amount.Currency);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new BudgetResetEvent(Id, CategoryId));
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new BudgetActivatedEvent(Id, CategoryId));
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new BudgetDeactivatedEvent(Id, CategoryId));
    }

    public void ExtendPeriod(BudgetPeriod newPeriod)
    {
        // Check if active
        if (!IsActive)
            throw new InactiveBudgetException(Id, Name, "extend period");

        // Validate period
        if (!Enum.IsDefined(typeof(BudgetPeriod), newPeriod))
            throw new InvalidBudgetPeriodException(newPeriod);

        var oldPeriod = Period;
        Period = newPeriod;

        // Recalculate end date from start date
        EndDate = CalculateEndDate(StartDate, newPeriod);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new BudgetPeriodChangedEvent(Id, oldPeriod, newPeriod));
    }

    // Query methods
    public bool IsExceeded()
    {
        return SpentAmount.Amount > Amount.Amount;
    }

    private bool WasExceeded(Money previousSpent)
    {
        return previousSpent.Amount > Amount.Amount;
    }

    public decimal GetPercentageUsed()
    {
        if (Amount.Amount == 0) return 0;
        return Math.Round((SpentAmount.Amount / Amount.Amount) * 100, 2);
    }

    private decimal GetPercentageUsed(Money spent)
    {
        if (Amount.Amount == 0) return 0;
        return Math.Round((spent.Amount / Amount.Amount) * 100, 2);
    }

    public Money GetRemainingAmount()
    {
        var remaining = Amount.Amount - SpentAmount.Amount;
        return new Money(remaining > 0 ? remaining : 0, Amount.Currency);
    }

    public bool IsPeriodExpired()
    {
        return DateTime.UtcNow > EndDate;
    }

    public bool IsPeriodActive()
    {
        return DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
    }

    public int GetDaysRemaining()
    {
        if (IsPeriodExpired())
            return 0;

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