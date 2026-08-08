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
        public Money Balance { get; private set; }
        public Money InitialBalance { get; private set; }

        public SavingsDetails? SavingsDetails { get; private set; }
        public bool IsActive { get; private set; }
        public byte[]? RowVersion { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Account() { }

        private Account(
            Guid accountId,
            Guid userId,
            string name,
            EnAccountType type,
            Money balance,
            Money initialBalance,
            bool isActive,
            SavingsDetails? savingsDetails,
            DateTime createdAt,
            DateTime? updatedAt,
            byte[]? rowVersion
            )
        {
            Id = accountId;
            UserId = userId;
            Name = name;
            Type = type;
            Balance = balance;
            InitialBalance = initialBalance;
            IsActive = isActive;
            SavingsDetails = savingsDetails;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            RowVersion = rowVersion;
        }

        // Reconstitute from persistence
        public static Account Load(
            Guid id,
            Guid userId,
            string name,
            EnAccountType type,
            Money balance,
            Money initialBalance,
            bool isActive,
            DateTime createdAt,
            DateTime? updatedAt = null,
            SavingsDetails? savingsDetails = null,
            byte[]? rowVersion = null
            )
        {
            return new Account(id, userId, name, type, balance,initialBalance, isActive,savingsDetails, createdAt, updatedAt,rowVersion);
        }

        // Create new account
        public static Account Create(
            Guid userId,
            string name,
            EnAccountType type,
            Money initialBalance,
            SavingsDetails? savingsDetails = null)
        {
            // validation
            ValidateAccountName(name);
            var account = new Account
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = name.Trim(),
                Type = type,
                Balance = initialBalance,
                InitialBalance = initialBalance,
                IsActive = true,
                SavingsDetails = savingsDetails,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            account.AddDomainEvent(new AccountCreated(
                account.Id,
                account.Name,
                account.Type,
                account.Balance));

            return account;
        }
        private static void ValidateAccountName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidAccountNameException(name, "Account name cannot be empty");

            if (name.Length > 100)
                throw new InvalidAccountNameException(name, "Account name cannot exceed 100 characters");


        }
        private static readonly Dictionary<EnAccountType, EnAccountType[]> AllowedAccountTypeTransitions = new()
        {
            [EnAccountType.Credit] = [EnAccountType.Cash, EnAccountType.Savings,EnAccountType.Other],
            [EnAccountType.Savings] = [EnAccountType.Checking, EnAccountType.Other],
            [EnAccountType.Cash] = [EnAccountType.Checking, EnAccountType.Other],
            [EnAccountType.Investment] = [EnAccountType.Other],
            [EnAccountType.Credit] = [],   // no transitions allowed
            [EnAccountType.Loan] = [],   // no transitions allowed
            [EnAccountType.Other] = [EnAccountType.Checking, EnAccountType.Savings, EnAccountType.Cash]
        };
        
        public void ApplyTransaction(Money amount, EnTransactionType type)
        {
            if (!IsActive)
                throw new InactiveAccountException(Id, Name);

            if (amount.Amount <= 0)
                throw new InvalidTransactionAmountException(amount);

            var previousBalance = Balance;

            if (type == EnTransactionType.Expense || type == EnTransactionType.DebitAdjustment)
            {
                if (!HasSufficientBalance(amount))
                    throw new InsufficientBalanceException(Id, amount, Balance);

                Balance = Balance.Subtract(amount);
            }
            else
            {
                Balance = Balance.Add(amount);
            }

            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new AccountBalanceChangedEvent(
                Id,
                previousBalance,
                Balance,
                type,
                amount));
        }
        private void ValidateAccountTypeTransition(EnAccountType newType)
        {
            if(Type != newType)
            {
                if (!AllowedAccountTypeTransitions[Type].Contains(newType))
                {
                    throw new InvalidAccountTypeTransitionException(Id, newType);
                }
            }
        }

        public void UpdateDetails(string name , EnAccountType type)
        {
            ValidateAccountName(name);
            ValidateAccountTypeTransition(type);
            Name = name.Trim();
            Type = type;
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
                throw new InvalidBalanceAdjustmentException(Id, Balance, newBalance, "Reason is required");

            var previousBalance = Balance;
            Balance = newBalance;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new AccountBalanceAdjustedEvent(Id, previousBalance, newBalance, reason));
        }

        public void TransferTo(Account targetAccount, Money amount)
        {
            if (!IsActive)
                throw new InactiveAccountException(Id, Name);

            if (!targetAccount.IsActive)
                throw new InactiveAccountException(targetAccount.Id, targetAccount.Name);

            if (Id == targetAccount.Id)
                throw new SelfTransferException(Id);

            if (amount.Amount <= 0)
                throw new InvalidTransactionAmountException(amount);

            if (!HasSufficientBalance(amount))
                throw new InsufficientBalanceException(Id, amount, Balance);

            ApplyTransaction(amount, EnTransactionType.Expense);
            targetAccount.ApplyTransaction(amount, EnTransactionType.Income);

            AddDomainEvent(new AccountTransferEvent(Id, targetAccount.Id, amount));
        }
        private void EnsureSavingsAccount()
        {
            if (Type != EnAccountType.Savings)
                throw new InvalidOperationException(
                    "Operation is only valid for savings accounts.");

            if (SavingsDetails is null)
                throw new InvalidOperationException(
                    "Savings account is missing SavingsDetails.");
        }
        public void AccrueInterest(Money amount)
        {
            EnsureSavingsAccount();

            SavingsDetails!.AccrueInterest(amount);

            UpdatedAt = DateTime.UtcNow;
        }
        public bool HasSufficientBalance(Money amount) => Balance.Amount >= amount.Amount;

        public decimal GetAvailableBalance() => Balance.Amount;

        public bool IsOverdrawn() => Balance.Amount < 0;

    }

}

