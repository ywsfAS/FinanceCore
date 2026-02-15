using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;


namespace FinanceCore.Domain.Exceptions
{
    // When transaction doesn't exist
    public class TransactionNotFoundException : DomainException
    {
        public Guid TransactionId { get; }

        public TransactionNotFoundException(Guid transactionId)
            : base($"Transaction with ID '{transactionId}' was not found.")
        {
            TransactionId = transactionId;
        }
    }

    // When trying to modify voided transaction
    public class VoidedTransactionException : DomainException
    {
        public Guid TransactionId { get; }

        public VoidedTransactionException(Guid transactionId)
            : base($"Cannot modify transaction {transactionId} because it has been voided.")
        {
            TransactionId = transactionId;
        }

        public VoidedTransactionException(Guid transactionId, string operation)
            : base($"Cannot {operation} transaction {transactionId} because it has been voided.")
        {
            TransactionId = transactionId;
        }
    }

    // When transaction already voided
    public class TransactionAlreadyVoidedException : DomainException
    {
        public Guid TransactionId { get; }

        public TransactionAlreadyVoidedException(Guid transactionId)
            : base($"Transaction {transactionId} is already voided.")
        {
            TransactionId = transactionId;
        }
    }

    // When transaction amount is invalid
    public class InvalidTransactionAmountException : DomainException
    {
        public decimal Amount { get; }

        public InvalidTransactionAmountException(decimal amount)
            : base($"Invalid transaction amount: {amount}. Amount must be positive.")
        {
            Amount = amount;
        }

        public InvalidTransactionAmountException(decimal amount, string reason)
            : base($"Invalid transaction amount: {amount}. {reason}")
        {
            Amount = amount;
        }
    }

    // When transaction date is invalid
    public class InvalidTransactionDateException : DomainException
    {
        public DateTime Date { get; }

        public InvalidTransactionDateException(DateTime date, string reason)
            : base($"Invalid transaction date: {date:yyyy-MM-dd}. {reason}")
        {
            Date = date;
        }
    }

    // When trying to change transaction currency
    public class TransactionCurrencyChangeException : DomainException
    {
        public Guid TransactionId { get; }
        public EnCurrency CurrentCurrency { get; }
        public EnCurrency AttemptedCurrency { get; }

        public TransactionCurrencyChangeException(
            Guid transactionId,
            EnCurrency currentCurrency,
            EnCurrency attemptedCurrency)
            : base($"Cannot change transaction currency from {currentCurrency} to {attemptedCurrency}.")
        {
            TransactionId = transactionId;
            CurrentCurrency = currentCurrency;
            AttemptedCurrency = attemptedCurrency;
        }
    }

    // When void reason is missing
    public class VoidReasonRequiredException : DomainException
    {
        public Guid TransactionId { get; }

        public VoidReasonRequiredException(Guid transactionId)
            : base($"A reason is required to void transaction {transactionId}.")
        {
            TransactionId = transactionId;
        }
    }

    // When category is invalid or not found
    public class InvalidTransactionCategoryException : DomainException
    {
        public Guid CategoryId { get; }

        public InvalidTransactionCategoryException(Guid categoryId)
            : base($"Invalid category ID: {categoryId}. Category does not exist or is inactive.")
        {
            CategoryId = categoryId;
        }

        public InvalidTransactionCategoryException(Guid categoryId, string reason)
            : base($"Invalid category {categoryId}: {reason}")
        {
            CategoryId = categoryId;
        }
    }

    // When transaction type doesn't match category type
    public class TransactionCategoryTypeMismatchException : DomainException
    {
        public EnTransactionType TransactionType { get; }
        public CategoryType CategoryType { get; }

        public TransactionCategoryTypeMismatchException(
            EnTransactionType transactionType,
            CategoryType categoryType)
            : base($"Transaction type {transactionType} does not match category type {categoryType}.")
        {
            TransactionType = transactionType;
            CategoryType = categoryType;
        }
    }

    // When transaction cannot be deleted (has dependencies)
    public class TransactionHasDependenciesException : DomainException
    {
        public Guid TransactionId { get; }
        public string Dependencies { get; }

        public TransactionHasDependenciesException(Guid transactionId, string dependencies)
            : base($"Cannot delete transaction {transactionId}. It has dependencies: {dependencies}")
        {
            TransactionId = transactionId;
            Dependencies = dependencies;
        }
    }

    // When transaction date is in the future (if you want to restrict this)
    public class FutureTransactionDateException : DomainException
    {
        public DateTime Date { get; }

        public FutureTransactionDateException(DateTime date)
            : base($"Transaction date cannot be in the future: {date:yyyy-MM-dd}")
        {
            Date = date;
        }
    }

    // When transaction description is invalid
    public class InvalidTransactionDescriptionException : DomainException
    {
        public string Description { get; }

        public InvalidTransactionDescriptionException(string description, string reason)
            : base($"Invalid transaction description '{description}': {reason}")
        {
            Description = description;
        }
    }

    // When duplicate transaction is detected
    public class DuplicateTransactionException : DomainException
    {
        public Guid AccountId { get; }
        public Money Amount { get; }
        public DateTime Date { get; }

        public DuplicateTransactionException(Guid accountId, Money amount, DateTime date)
            : base($"Duplicate transaction detected for account {accountId}: {amount.Amount} {amount.Currency} on {date:yyyy-MM-dd}")
        {
            AccountId = accountId;
            Amount = amount;
            Date = date;
        }
    }
}
