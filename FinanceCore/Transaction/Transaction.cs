using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.Transaction;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Transactions;

public class Transaction : AggregateRoot
{
    public Guid AccountId { get; private set; }
    public Money Amount { get; private set; }
    public Guid CategoryId { get; private set; }
    public EnTransactionType Type { get; private set; }
    public DateTime Date { get; private set; }
    public string? Description { get; private set; }
    public string? Notes { get; private set; }
    public EnTransactionStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Transaction() { }

    private Transaction(    
        Guid TransactionId,
        Guid accountId,
        Money amount,
        Guid categoryId,
        EnTransactionType type,
        DateTime date,
        string? description = null,
        string? notes = null
        )
    {
        Id = TransactionId;
        AccountId = accountId;
        Amount = amount;
        CategoryId = categoryId;
        Type = type;
       Description = description;
        Notes = notes;
        Date = date;
    }
    public static Transaction Create(
        Guid TransactionId,
        Guid accountId,
        Money amount,
        Guid categoryId,
        EnTransactionType type,
        DateTime date,
        string? description = null,
        string? notes = null
        )
    {
        return new Transaction(TransactionId,accountId,amount,categoryId,type,date,description,notes);

    }
    public static Transaction Create(
        Guid accountId,
        Money amount,
        Guid categoryId,
        EnTransactionType type,
        DateTime? date = null,
        string? description = null,
        string? notes = null)
    {
        // Validate account ID
        if (accountId == Guid.Empty)
            throw new ArgumentException("Account ID cannot be empty.", nameof(accountId));

        // Validate category ID
        if (categoryId == Guid.Empty)
            throw new InvalidTransactionCategoryException(categoryId, "Category ID cannot be empty");

        // Validate amount
        if (amount.Amount <= 0)
            throw new InvalidTransactionAmountException(amount.Amount);

        // Validate date (optional: prevent future dates)
        var transactionDate = date ?? DateTime.UtcNow;
        if (transactionDate > DateTime.UtcNow.AddDays(1)) 
            throw new FutureTransactionDateException(transactionDate);

   
        if (description != null && description.Length > 500)
            throw new InvalidTransactionDescriptionException(
                description,
                "Description cannot exceed 500 characters");

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            AccountId = accountId,
            Amount = amount,
            CategoryId = categoryId,
            Type = type,
            Date = transactionDate,
            Description = description?.Trim(),
            Notes = notes?.Trim(),
            Status = EnTransactionStatus.Completed,
            CreatedAt = DateTime.UtcNow
        };

        transaction.AddDomainEvent(new TransactionCreatedEvent(
            transaction.Id,
            transaction.AccountId,
            transaction.Amount,
            transaction.Type,
            transaction.Date));

        return transaction;
    }

    public void Update(
        Money? amount = null,
        Guid? categoryId = null,
        DateTime? date = null,
        string? description = null,
        string? notes = null)
    {
        // Check if voided
        if (Status == EnTransactionStatus.Voided)
            throw new VoidedTransactionException(Id, "update");

        var hasChanges = false;

        // Update amount
        if (amount != null && amount != Amount)
        {
            if (amount.Amount <= 0)
                throw new InvalidTransactionAmountException(amount.Amount);

            if (amount.Currency != Amount.Currency)
                throw new TransactionCurrencyChangeException(Id, Amount.Currency, amount.Currency);

            var oldAmount = Amount;
            Amount = amount;
            hasChanges = true;

            AddDomainEvent(new TransactionAmountChangedEvent(
                Id,
                AccountId,
                oldAmount,
                Amount));
        }

        // Update category
        if (categoryId.HasValue && categoryId.Value != CategoryId)
        {
            if (categoryId.Value == Guid.Empty)
                throw new InvalidTransactionCategoryException(categoryId.Value, "Category ID cannot be empty");

            var oldCategoryId = CategoryId;
            CategoryId = categoryId.Value;
            hasChanges = true;

            AddDomainEvent(new TransactionCategoryChangedEvent(
                Id,
                oldCategoryId,
                CategoryId));
        }

        // Update date
        if (date.HasValue && date.Value != Date)
        {
            if (date.Value > DateTime.UtcNow.AddDays(1))
                throw new FutureTransactionDateException(date.Value);

            Date = date.Value;
            hasChanges = true;
        }

        // Update description
        if (description != null)
        {
            if (description.Length > 500)
                throw new InvalidTransactionDescriptionException(
                    description,
                    "Description cannot exceed 500 characters");

            Description = description.Trim();
            hasChanges = true;
        }

        // Update notes
        if (notes != null)
        {
            Notes = notes.Trim();
            hasChanges = true;
        }

        if (hasChanges)
        {
            UpdatedAt = DateTime.UtcNow;
            AddDomainEvent(new TransactionUpdatedEvent(Id, AccountId));
        }
    }

    public void Void(string reason)
    {
        // Check if already voided
        if (Status == EnTransactionStatus.Voided)
            throw new TransactionAlreadyVoidedException(Id);

        // Validate reason
        if (string.IsNullOrWhiteSpace(reason))
            throw new VoidReasonRequiredException(Id);

        if (reason.Length > 500)
            throw new ArgumentException("Void reason cannot exceed 500 characters.", nameof(reason));

        var previousStatus = Status;
        Status = EnTransactionStatus.Voided;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new TransactionVoidedEvent(
            Id,
            AccountId,
            Amount,
            Type,
            reason,
            previousStatus));
    }

    public bool IsVoided()
    {
        return Status == EnTransactionStatus.Voided;
    }

    public bool IsCompleted()
    {
        return Status == EnTransactionStatus.Completed;
    }
}