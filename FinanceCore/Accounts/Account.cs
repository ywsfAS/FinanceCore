using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.Account;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Accounts;

public class Account : AggregateRoot
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public EnAccountType Type { get; private set; }
    public EnCurrency Currency { get; private set; }
    public Money Balance { get; private set; }
    public Money InitialBalance { get; private set; }
    public bool IsActive { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastTransactionAt { get; private set; }

    public string? BankName { get; private set; }
    public string? AccountNumberLastFour { get; private set; }

    private Account() { }

    public static Account Create(
        Guid userId,
        string name,
        EnAccountType type,
        EnCurrency currency,
        decimal initialBalance = 0,
        string? description = null,
        string? bankName = null,
        string? accountNumberLastFour = null)
    {
        // Validate name
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidAccountNameException(name, "Account name cannot be empty");

        if (name.Length > 100)
            throw new InvalidAccountNameException(name, "Account name cannot exceed 100 characters");

        // Validate account number
        if (accountNumberLastFour != null && accountNumberLastFour.Length != 4)
            throw new InvalidAccountNumberException(accountNumberLastFour);

        var account = new Account
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = name.Trim(),
            Type = type,
            Currency = currency,
            InitialBalance = new Money(initialBalance, currency),
            Balance = new Money(initialBalance, currency),
            IsActive = true,
            Description = description?.Trim(),
            BankName = bankName?.Trim(),
            AccountNumberLastFour = accountNumberLastFour,
            CreatedAt = DateTime.UtcNow
        };

        account.AddDomainEvent(new AccountCreatedEvent(
            account.Id,
            account.Name,
            account.Type,
            account.Currency,
            account.InitialBalance));

        return account;
    }

    public void ApplyTransaction(Money amount, EnTransactionType type)
    {
        // Check if account is active
        if (!IsActive)
            throw new InactiveAccountException(Id, Name);

        // Check currency match
        if (amount.Currency != Currency)
            throw new CurrencyMismatchException(
                Currency,
                amount.Currency,
                $"Transaction currency ({amount.Currency}) must match account currency ({Currency})");

        // Check amount is positive
        if (amount.Amount <= 0)
            throw new InvalidTransferAmountException(amount);

        var previousBalance = Balance;

        // For expenses, check sufficient balance
        if (type == EnTransactionType.Expense)
        {
            if (!HasSufficientBalance(amount))
                throw new InsufficientBalanceException(Id, amount, Balance);

            Balance = Balance.Subtract(amount);
        }
        else
        {
            Balance = Balance.Add(amount);
        }

        LastTransactionAt = DateTime.UtcNow;

        AddDomainEvent(new AccountBalanceChangedEvent(
            Id,
            previousBalance,
            Balance,
            type,
            amount));
    }

    public void UpdateDetails(
        string? name = null,
        string? description = null,
        string? bankName = null,
        string? accountNumberLastFour = null)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidAccountNameException(name, "Account name cannot be empty");

            if (name.Length > 100)
                throw new InvalidAccountNameException(name, "Account name cannot exceed 100 characters");

            Name = name.Trim();
        }

        if (description != null)
            Description = description.Trim();

        if (bankName != null)
            BankName = bankName.Trim();

        if (accountNumberLastFour != null)
        {
            if (accountNumberLastFour.Length != 4)
                throw new InvalidAccountNumberException(accountNumberLastFour);

            AccountNumberLastFour = accountNumberLastFour;
        }

        AddDomainEvent(new AccountUpdatedEvent(Id, Name));
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        AddDomainEvent(new AccountActivatedEvent(Id, Name));
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        AddDomainEvent(new AccountDeactivatedEvent(Id, Name));
    }

    public void AdjustBalance(Money newBalance, string reason)
    {
        if (!IsActive)
            throw new InactiveAccountException(Id, Name);

        if (newBalance.Currency != Currency)
            throw new CurrencyMismatchException(Currency, newBalance.Currency);

        if (string.IsNullOrWhiteSpace(reason))
            throw new InvalidBalanceAdjustmentException(
                Id,
                Balance,
                newBalance,
                "Reason for balance adjustment is required");

        var previousBalance = Balance;
        Balance = newBalance;

        AddDomainEvent(new AccountBalanceAdjustedEvent(
            Id,
            previousBalance,
            newBalance,
            reason));
    }

    public void TransferTo(Account targetAccount, Money amount)
    {
        // Check source account is active
        if (!IsActive)
            throw new InactiveAccountException(Id, Name);

        // Check target account is active
        if (!targetAccount.IsActive)
            throw new InactiveAccountException(targetAccount.Id, targetAccount.Name);

        // Check not transferring to same account
        if (Id == targetAccount.Id)
            throw new SelfTransferException(Id);

        // Check currency match
        if (amount.Currency != Currency)
            throw new CurrencyMismatchException(
                Currency,
                amount.Currency,
                "Transfer amount currency must match source account currency");

        // Check positive amount
        if (amount.Amount <= 0)
            throw new InvalidTransferAmountException(amount);

        // Check sufficient balance
        if (!HasSufficientBalance(amount))
            throw new InsufficientBalanceException(Id, amount, Balance);

        // Check same currency (for now)
        if (amount.Currency != targetAccount.Currency)
            throw new CurrencyMismatchException(
                Currency,
                targetAccount.Currency,
                "Currency conversion not supported. Both accounts must use the same currency");

        // Apply transactions
        ApplyTransaction(amount, EnTransactionType.Expense);
        targetAccount.ApplyTransaction(amount, EnTransactionType.Income);

        AddDomainEvent(new AccountTransferEvent(
            Id,
            targetAccount.Id,
            amount));
    }

    public bool HasSufficientBalance(Money amount)
    {
        if (amount.Currency != Currency)
            return false;

        return Balance.Amount >= amount.Amount;
    }

    public Money GetAvailableBalance()
    {
        return Balance;
    }

    public bool IsOverdrawn()
    {
        return Balance.Amount < 0;
    }
}