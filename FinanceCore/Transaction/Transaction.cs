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

        public Guid? BatchId { get; private set; } = null;

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
        public static Transaction Load(
            Guid transactionId,
            Guid accountId,
            Guid? toAccountId,
            Money amount,
            Guid? categoryId,
            EnTransactionType type,
            DateTime date,
            string? description,
            DateTime createdAt,
            DateTime? updatedAt = null)
        {
            return new Transaction(
                transactionId, accountId, toAccountId,
                amount, categoryId, type,
                date, description, createdAt, updatedAt);
        }

        // Create new transaction
        public static Transaction Create(
            Guid accountId,
            Guid? toAccountId,
            Money amount,
            Guid? categoryId,
            EnTransactionType type,
            DateTime? date = null,
            string? description = null,
            Guid? batchId = null
            )
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

            if (amount.IsLessOrEqual(Money.Zero(amount.Currency)))
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
                Amount = amount,
                CategoryId = categoryId,
                Type = type,
                Date = transactionDate,
                Description = description?.Trim(),
                BatchId = batchId,
                CreatedAt = DateTime.UtcNow
            };

            transaction.AddDomainEvent(new TransactionCreatedEvent(
                transaction.Id,
                transaction.AccountId,
                transaction.ToAccountId,
                transaction.Amount,
                transaction.Type,
                transaction.Date));

            return transaction;
        }
        public static Transaction CreateTransfer(
        Guid fromAccountId,
        Guid toAccountId,
        Money money,
        DateTime date,
        string? description)
        {
            return new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = fromAccountId,
                ToAccountId = toAccountId,
                Amount = money,
                Type = EnTransactionType.Transfer,
                Date = date,
                Description = description,
                CategoryId = null
            };
        }

        public void Update(
            Money? amount = null,
            Guid? categoryId = null,
            DateTime? date = null,
            string? description = null)
        {
            var hasChanges = false;
            if (amount is not null && !amount.Equals(Amount))
            {
                if (amount.IsLessOrEqual(Money.Zero(amount.Currency)))
                    throw new InvalidTransactionAmountException(amount);

                var oldAmount = Amount;

                Amount = amount;

                hasChanges = true;

                AddDomainEvent(
                    new TransactionAmountChangedEvent(
                        Id,
                        AccountId,
                        oldAmount,
                        Amount));
            }

            if (categoryId.HasValue && categoryId.Value != CategoryId)
            {
                if ( categoryId.HasValue &&categoryId.Value == Guid.Empty)
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

