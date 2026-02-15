using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Exceptions
{
    // When account doesn't exist
    public class AccountNotFoundException : DomainException
    {
        public Guid AccountId { get; }

        public AccountNotFoundException(Guid accountId)
            : base($"Account with ID '{accountId}' was not found.")
        {
            AccountId = accountId;
        }
    }

    // When trying to use inactive account
    public class InactiveAccountException : DomainException
    {
        public Guid AccountId { get; }
        public string AccountName { get; }

        public InactiveAccountException(Guid accountId, string accountName)
            : base($"Account '{accountName}' (ID: {accountId}) is inactive and cannot be used.")
        {
            AccountId = accountId;
            AccountName = accountName;
        }
    }

    // When account has insufficient balance
    public class InsufficientBalanceException : DomainException
    {
        public Guid AccountId { get; }
        public Money Required { get; }
        public Money Available { get; }

        public InsufficientBalanceException(Guid accountId, Money required, Money available)
            : base($"Account has insufficient balance. Required: {required.Amount} {required.Currency}, Available: {available.Amount} {available.Currency}")
        {
            AccountId = accountId;
            Required = required;
            Available = available;
        }
    }

    // When currencies don't match
    public class CurrencyMismatchException : DomainException
    {
        public EnCurrency ExpectedCurrency { get; }
        public EnCurrency ActualCurrency { get; }

        public CurrencyMismatchException(EnCurrency expected, EnCurrency actual)
            : base($"Currency mismatch. Expected: {expected}, Actual: {actual}")
        {
            ExpectedCurrency = expected;
            ActualCurrency = actual;
        }

        public CurrencyMismatchException(EnCurrency expected, EnCurrency actual, string customMessage)
            : base(customMessage)
        {
            ExpectedCurrency = expected;
            ActualCurrency = actual;
        }
    }

    // When trying to transfer between same account
    public class SelfTransferException : DomainException
    {
        public Guid AccountId { get; }

        public SelfTransferException(Guid accountId)
            : base($"Cannot transfer to the same account (ID: {accountId}).")
        {
            AccountId = accountId;
        }
    }

    // When account name is invalid
    public class InvalidAccountNameException : DomainException
    {
        public string ProvidedName { get; }

        public InvalidAccountNameException(string providedName, string reason)
            : base($"Invalid account name '{providedName}': {reason}")
        {
            ProvidedName = providedName;
        }
    }

    // When duplicate account exists
    public class DuplicateAccountException : DomainException
    {
        public string AccountName { get; }
        public Guid UserId { get; }

        public DuplicateAccountException(string accountName, Guid userId)
            : base($"An account with name '{accountName}' already exists for user {userId}.")
        {
            AccountName = accountName;
            UserId = userId;
        }
    }

    // When invalid account number format
    public class InvalidAccountNumberException : DomainException
    {
        public string ProvidedAccountNumber { get; }

        public InvalidAccountNumberException(string providedNumber)
            : base($"Invalid account number format: '{providedNumber}'. Must be exactly 4 digits.")
        {
            ProvidedAccountNumber = providedNumber;
        }
    }

    // When balance adjustment is invalid
    public class InvalidBalanceAdjustmentException : DomainException
    {
        public Guid AccountId { get; }
        public Money CurrentBalance { get; }
        public Money NewBalance { get; }

        public InvalidBalanceAdjustmentException(Guid accountId, Money currentBalance, Money newBalance, string reason)
            : base($"Cannot adjust balance from {currentBalance.Amount} to {newBalance.Amount}: {reason}")
        {
            AccountId = accountId;
            CurrentBalance = currentBalance;
            NewBalance = newBalance;
        }
    }

    // When transfer amount is invalid
    public class InvalidTransferAmountException : DomainException
    {
        public Money Amount { get; }

        public InvalidTransferAmountException(Money amount)
            : base($"Invalid transfer amount: {amount.Amount} {amount.Currency}. Amount must be positive.")
        {
            Amount = amount;
        }
    }

    // When account cannot be deleted (has dependencies)
    public class AccountHasDependenciesException : DomainException
    {
        public Guid AccountId { get; }
        public string Dependencies { get; }

        public AccountHasDependenciesException(Guid accountId, string dependencies)
            : base($"Cannot delete account {accountId}. It has dependencies: {dependencies}")
        {
            AccountId = accountId;
            Dependencies = dependencies;
        }
    }
}
