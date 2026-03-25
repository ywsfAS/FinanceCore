using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Exceptions
{

    // When budget doesn't exist
    public class BudgetNotFoundException : DomainException
    {
        public Guid BudgetId { get; }

        public BudgetNotFoundException(Guid budgetId)
            : base($"Budget with ID '{budgetId}' was not found.")
        {
            BudgetId = budgetId;
        }
    }

    // When trying to operate on inactive budget
    public class InactiveBudgetException : DomainException
    {
        public Guid BudgetId { get; }
        public string BudgetName { get; }

        public InactiveBudgetException(Guid budgetId, string budgetName)
            : base($"Budget '{budgetName}' (ID: {budgetId}) is inactive.")
        {
            BudgetId = budgetId;
            BudgetName = budgetName;
        }

        public InactiveBudgetException(Guid budgetId, string budgetName, string operation)
            : base($"Cannot {operation} for inactive budget '{budgetName}' (ID: {budgetId}).")
        {
            BudgetId = budgetId;
            BudgetName = budgetName;
        }
    }

    // When budget amount is invalid
    public class InvalidBudgetAmountException : DomainException
    {
        public decimal Amount { get; }

        public InvalidBudgetAmountException(decimal amount)
            : base($"Invalid budget amount: {amount}. Amount must be positive.")
        {
            Amount = amount;
        }

        public InvalidBudgetAmountException(decimal amount, string reason)
            : base($"Invalid budget amount: {amount}. {reason}")
        {
            Amount = amount;
        }
    }

    // When budget name is invalid
    public class InvalidBudgetNameException : DomainException
    {
        public string ProvidedName { get; }

        public InvalidBudgetNameException(string providedName, string reason)
            : base($"Invalid budget name '{providedName}': {reason}")
        {
            ProvidedName = providedName;
        }
    }

    // When budget period is invalid
    public class InvalidBudgetPeriodException : DomainException
    {
        public EnPeriod Period { get; }

        public InvalidBudgetPeriodException(EnPeriod period)
            : base($"Invalid budget period: {period}")
        {
            Period = period;
        }

        public InvalidBudgetPeriodException(string reason)
            : base($"Invalid budget period: {reason}")
        {
        }
    }

    // When budget date range is invalid
    public class InvalidBudgetDateRangeException : DomainException
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public InvalidBudgetDateRangeException(DateTime startDate, DateTime endDate)
            : base($"Invalid budget date range: Start date {startDate:yyyy-MM-dd} must be before end date {endDate:yyyy-MM-dd}")
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public InvalidBudgetDateRangeException(DateTime startDate, DateTime endDate, string reason)
            : base($"Invalid budget date range ({startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}): {reason}")
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    // When trying to add spending with wrong currency
    public class BudgetCurrencyMismatchException : DomainException
    {
        public Guid BudgetId { get; }
        public EnCurrency BudgetCurrency { get; }
        public EnCurrency SpendingCurrency { get; }

        public BudgetCurrencyMismatchException(Guid budgetId, EnCurrency budgetCurrency, EnCurrency spendingCurrency)
            : base($"Cannot add spending in {spendingCurrency} to budget with currency {budgetCurrency}")
        {
            BudgetId = budgetId;
            BudgetCurrency = budgetCurrency;
            SpendingCurrency = spendingCurrency;
        }
    }

    // When trying to change budget currency
    public class BudgetCurrencyChangeException : DomainException
    {
        public Guid BudgetId { get; }
        public EnCurrency CurrentCurrency { get; }
        public EnCurrency AttemptedCurrency { get; }

        public BudgetCurrencyChangeException(Guid budgetId, EnCurrency currentCurrency, EnCurrency attemptedCurrency)
            : base($"Cannot change budget currency from {currentCurrency} to {attemptedCurrency}")
        {
            BudgetId = budgetId;
            CurrentCurrency = currentCurrency;
            AttemptedCurrency = attemptedCurrency;
        }
    }

    // When budget is exceeded
    public class BudgetExceededException : DomainException
    {
        public Guid BudgetId { get; }
        public string CategoryName { get; }
        public Money BudgetAmount { get; }
        public Money SpentAmount { get; }

        public BudgetExceededException(Guid budgetId, string categoryName, Money budgetAmount, Money spentAmount)
            : base($"Budget '{categoryName}' exceeded. Budget: {budgetAmount.Amount}, Spent: {spentAmount.Amount}")
        {
            BudgetId = budgetId;
            CategoryName = categoryName;
            BudgetAmount = budgetAmount;
            SpentAmount = spentAmount;
        }
    }

    // When duplicate budget exists
    public class DuplicateBudgetException : DomainException
    {
        public Guid UserId { get; }
        public Guid CategoryId { get; }
        public EnPeriod Period { get; }

        public DuplicateBudgetException(Guid userId, Guid categoryId, EnPeriod period)
            : base($"A {period} budget for category {categoryId} already exists for user {userId}")
        {
            UserId = userId;
            CategoryId = categoryId;
            Period = period;
        }
    }

    // When budget period has expired
    public class BudgetPeriodExpiredException : DomainException
    {
        public Guid BudgetId { get; }
        public DateTime EndDate { get; }

        public BudgetPeriodExpiredException(Guid budgetId, DateTime endDate)
            : base($"Budget {budgetId} period has expired on {endDate:yyyy-MM-dd}")
        {
            BudgetId = budgetId;
            EndDate = endDate;
        }
    }

    // When budget cannot be deleted (has dependencies)
    public class BudgetHasDependenciesException : DomainException
    {
        public Guid BudgetId { get; }
        public string Dependencies { get; }

        public BudgetHasDependenciesException(Guid budgetId, string dependencies)
            : base($"Cannot delete budget {budgetId}. It has dependencies: {dependencies}")
        {
            BudgetId = budgetId;
            Dependencies = dependencies;
        }
    }

    // When invalid spending amount
    public class InvalidSpendingAmountException : DomainException
    {
        public decimal Amount { get; }

        public InvalidSpendingAmountException(decimal amount)
            : base($"Invalid spending amount: {amount}. Amount must be positive.")
        {
            Amount = amount;
        }
    }

    // When budget reset not allowed
    public class BudgetResetNotAllowedException : DomainException
    {
        public Guid BudgetId { get; }
        public string Reason { get; }

        public BudgetResetNotAllowedException(Guid budgetId, string reason)
            : base($"Cannot reset budget {budgetId}: {reason}")
        {
            BudgetId = budgetId;
            Reason = reason;
        }
    }

    // When budget amount update would make it less than spent
    public class BudgetAmountBelowSpentException : DomainException
    {
        public Guid BudgetId { get; }
        public Money NewAmount { get; }
        public Money SpentAmount { get; }

        public BudgetAmountBelowSpentException(Guid budgetId, Money newAmount, Money spentAmount)
            : base($"Cannot set budget amount to {newAmount.Amount} because {spentAmount.Amount} has already been spent")
        {
            BudgetId = budgetId;
            NewAmount = newAmount;
            SpentAmount = spentAmount;
        }
    }

    // When overlapping budgets exist
    public class OverlappingBudgetException : DomainException
    {
        public Guid UserId { get; }
        public Guid CategoryId { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public OverlappingBudgetException(Guid userId, Guid categoryId, DateTime startDate, DateTime endDate)
            : base($"A budget for category {categoryId} already exists overlapping with period {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}")
        {
            UserId = userId;
            CategoryId = categoryId;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
