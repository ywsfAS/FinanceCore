using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.Account;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Accounts
{

    public class Account : AggregateRoot
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public EnAccountType Type { get; private set; }
        public EnCurrency Currency { get; private set; }
        public Money Balance { get; private set; }
        public Money InitialBalance { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Account() { }

        private Account(
            Guid accountId,
            Guid userId,
            string name,
            EnAccountType type,
            EnCurrency currency,
            Money balance,
            Money initialBalance,
            bool isActive,
            DateTime createdAt,
            DateTime? updatedAt)
        {
            Id = accountId;
            UserId = userId;
            Name = name;
            Type = type;
            Currency = currency;
            Balance = balance;
            InitialBalance = initialBalance;
            IsActive = isActive;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        // Reconstitute from persistence
        public static Account Create(
            Guid id,
            Guid userId,
            string name,
            EnAccountType type,
            EnCurrency currency,
            decimal balance,
            decimal initialBalance,
            bool isActive,
            DateTime createdAt,
            DateTime? updatedAt = null)
        {
            return new Account(id, userId, name, type, currency, new Money(balance), new Money(initialBalance), isActive, createdAt, updatedAt);
        }

        // Create new account
        public static Account Create(
            Guid userId,
            string name,
            EnAccountType type,
            EnCurrency currency,
            decimal initialBalance = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidAccountNameException(name, "Account name cannot be empty");

            if (name.Length > 100)
                throw new InvalidAccountNameException(name, "Account name cannot exceed 100 characters");

            var account = new Account
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = name.Trim(),
                Type = type,
                Currency = currency,
                Balance = new Money(initialBalance),
                InitialBalance = new Money(initialBalance),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            account.AddDomainEvent(new AccountCreated(
                account.Id,
                account.Name,
                account.Type,
                account.Currency,
                account.Balance.Amount));

            return account;
        }

        public void ApplyTransaction(Money amount, EnTransactionType type)
        {
            if (!IsActive)
                throw new InactiveAccountException(Id, Name);

            if (amount.Amount <= 0)
                throw new InvalidTransactionAmountException(amount.Amount);

            var previousBalance = Balance;

            if (type == EnTransactionType.Expense)
            {
                if (!HasSufficientBalance(amount))
                    throw new InsufficientBalanceException(Id, amount.Amount, Balance.Amount);

                Balance = Balance.Subtract(amount);
            }
            else
            {
                Balance = Balance.Add(amount);
            }

            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new AccountBalanceChangedEvent(
                Id,
                previousBalance.Amount,
                Balance.Amount,
                type,
                amount.Amount));
        }

        public void UpdateDetails(string? name = null)
        {
            if (name != null)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new InvalidAccountNameException(name, "Account name cannot be empty");

                if (name.Length > 100)
                    throw new InvalidAccountNameException(name, "Account name cannot exceed 100 characters");

                Name = name.Trim();
            }

            UpdatedAt = DateTime.UtcNow;
            AddDomainEvent(new AccountUpdatedEvent(Id, Name));
        }

        public void Activate()
        {
            if (IsActive) return;
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
            AddDomainEvent(new AccountActivatedEvent(Id, Name));
        }

        public void Deactivate()
        {
            if (!IsActive) return;
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
            AddDomainEvent(new AccountDeactivatedEvent(Id, Name));
        }

        public void AdjustBalance(Money newBalance, string reason)
        {
            if (!IsActive)
                throw new InactiveAccountException(Id, Name);

            if (string.IsNullOrWhiteSpace(reason))
                throw new InvalidBalanceAdjustmentException(Id, Balance.Amount, newBalance.Amount, "Reason is required");

            var previousBalance = Balance;
            Balance = newBalance;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new AccountBalanceAdjustedEvent(Id, previousBalance.Amount, newBalance.Amount, reason));
        }

        public void TransferTo(Account targetAccount, Money amount)
        {
            if (!IsActive)
                throw new InactiveAccountException(Id, Name);

            if (!targetAccount.IsActive)
                throw new InactiveAccountException(targetAccount.Id, targetAccount.Name);

            if (Id == targetAccount.Id)
                throw new SelfTransferException(Id);

            if (Currency != targetAccount.Currency)
                throw new CurrencyMismatchException(Currency, targetAccount.Currency,
                    "Currency conversion not supported. Both accounts must use the same currency");

            if (amount.Amount <= 0)
                throw new InvalidTransactionAmountException(amount.Amount);

            if (!HasSufficientBalance(amount))
                throw new InsufficientBalanceException(Id, amount.Amount, Balance.Amount);

            ApplyTransaction(amount, EnTransactionType.Expense);
            targetAccount.ApplyTransaction(amount, EnTransactionType.Income);

            AddDomainEvent(new AccountTransferEvent(Id, targetAccount.Id, amount.Amount));
        }

        public bool HasSufficientBalance(Money amount) => Balance.Amount >= amount.Amount;

        public decimal GetAvailableBalance() => Balance.Amount;

        public bool IsOverdrawn() => Balance.Amount < 0;
    }

}

