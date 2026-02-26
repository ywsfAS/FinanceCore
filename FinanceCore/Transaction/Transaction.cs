using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.Transaction;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Transactions
{


    public class Transaction : AggregateRoot
    {
        public Guid AccountId { get; private set; }
        public Guid? ToAccountId { get; private set; }
        public Guid? CategoryId { get; private set; }
        public Money Amount { get; private set; }
        public EnTransactionType Type { get; private set; }
        public DateTime Date { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Transaction() { }

        private Transaction(
            Guid transactionId,
            Guid accountId,
            Guid? toAccountId,
            Money amount,
            Guid? categoryId,
            EnTransactionType type,
            DateTime date,
            string? description,
            DateTime createdAt,
            DateTime? updatedAt)
        {
            Id = transactionId;
            AccountId = accountId;
            ToAccountId = toAccountId;
            Amount = amount;
            CategoryId = categoryId;
            Type = type;
            Date = date;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        // Reconstitute from persistence
        public static Transaction Create(
            Guid transactionId,
            Guid accountId,
            Guid? toAccountId,
            decimal amount,
            Guid? categoryId,
            EnTransactionType type,
            DateTime date,
            string? description,
            DateTime createdAt,
            DateTime? updatedAt = null)
        {
            return new Transaction(
                transactionId, accountId, toAccountId,
                new Money(amount), categoryId, type,
                date, description, createdAt, updatedAt);
        }

        // Create new transaction
        public static Transaction Create(
            Guid accountId,
            Guid? toAccountId,
            decimal amount,
            Guid? categoryId,
            EnTransactionType type,
            DateTime? date = null,
            string? description = null)
        {
            if (accountId == Guid.Empty)
                throw new ArgumentException("Account ID cannot be empty.", nameof(accountId));

            if (type == EnTransactionType.Transfer && toAccountId == null)
                throw new ArgumentException("ToAccountId is required for transfer transactions.", nameof(toAccountId));

            if (type != EnTransactionType.Transfer && toAccountId != null)
                throw new ArgumentException("ToAccountId should only be set for transfer transactions.", nameof(toAccountId));

            if (toAccountId == accountId)
                throw new SelfTransferException(accountId);

            if (categoryId == Guid.Empty)
                throw new InvalidTransactionCategoryException();

            if (amount <= 0)
                throw new InvalidTransactionAmountException(amount);

            var transactionDate = date ?? DateTime.UtcNow;
            if (transactionDate > DateTime.UtcNow.AddDays(1))
                throw new FutureTransactionDateException(transactionDate);

            if (description != null && description.Length > 500)
                throw new InvalidTransactionDescriptionException(description,
                    "Description cannot exceed 500 characters");

            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                ToAccountId = toAccountId,
                Amount = new Money(amount),
                CategoryId = categoryId,
                Type = type,
                Date = transactionDate,
                Description = description?.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            transaction.AddDomainEvent(new TransactionCreatedEvent(
                transaction.Id,
                transaction.AccountId,
                transaction.ToAccountId,
                transaction.Amount.Amount,
                transaction.Type,
                transaction.Date));

            return transaction;
        }

        public void Update(
            decimal? amount = null,
            Guid? categoryId = null,
            DateTime? date = null,
            string? description = null)
        {
            var hasChanges = false;

            if (amount.HasValue && amount.Value != Amount.Amount)
            {
                if (amount.Value <= 0)
                    throw new InvalidTransactionAmountException(amount.Value);

                var oldAmount = Amount;
                Amount = new Money(amount.Value);
                hasChanges = true;

                AddDomainEvent(new TransactionAmountChangedEvent(
                    Id, AccountId, oldAmount.Amount, Amount.Amount));
            }

            if (categoryId.HasValue && categoryId.Value != CategoryId)
            {
                if (categoryId.Value == Guid.Empty)
                    throw new InvalidTransactionCategoryException(categoryId.Value, "Category ID cannot be empty");

                var oldCategoryId = CategoryId;
                CategoryId = categoryId.Value;
                hasChanges = true;
                   if(CategoryId != Guid.Empty && oldCategoryId != Guid.Empty)
                {
                    AddDomainEvent(new TransactionCategoryChangedEvent(Id, oldCategoryId, CategoryId));
                }
            }

            if (date.HasValue && date.Value != Date)
            {
                if (date.Value > DateTime.UtcNow.AddDays(1))
                    throw new FutureTransactionDateException(date.Value);

                Date = date.Value;
                hasChanges = true;
            }

            if (description != null)
            {
                if (description.Length > 500)
                    throw new InvalidTransactionDescriptionException(description,
                        "Description cannot exceed 500 characters");

                Description = description.Trim();
                hasChanges = true;
            }

            if (hasChanges)
            {
                UpdatedAt = DateTime.UtcNow;
                AddDomainEvent(new TransactionUpdatedEvent(Id, AccountId));
            }
        }

        public bool IsTransfer() => Type == EnTransactionType.Transfer;
        public bool IsExpense() => Type == EnTransactionType.Expense;
        public bool IsIncome() => Type == EnTransactionType.Income;
    }
}

