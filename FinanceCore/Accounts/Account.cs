using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.Account;
using FinanceCore.Domain.Exceptions;
using System.Linq.Expressions;

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
        public CreditDetails? CreditDetails { get; private set; }
        public LoanDetails? LoanDetails { get; private set; }
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
            CreditDetails? creditDetails = null,
            LoanDetails? loanDetails = null,
            byte[]? rowVersion = null
            )
        {
            var account = new Account(id, userId, name, type, balance, initialBalance, isActive, savingsDetails, createdAt, updatedAt, rowVersion);
            account.CreditDetails = creditDetails;
            account.LoanDetails = loanDetails;
            return account;
        }

        // Create new account
        public static Account Create(
            Guid userId,
            string name,
            EnAccountType type,
            Money initialBalance,
            decimal? interestRate = null,
            EnInterestCreditFrequency? creditFrequency = null,
            EnInterestAccrualFrequency? accrualFrequency = null,
            decimal? creditLimit = null,
            decimal? fee = null,
            EnPeriod? feePeriod = null,
            decimal? principalAmount = null,
            decimal? loanInterestRate = null,
            int? termInMonths = null,
            EnRepaymentFrequency? repaymentFrequency = null,
            DateTime? startDate = null,
            decimal? regularPaymentAmount = null,
            DateTime? maturityDate = null,
            DateTime? nextPaymentDate = null
            )
        {
            ValidateAccountName(name);

            SavingsDetails? savingsDetails = null;

            if (type == EnAccountType.Savings)
            {
                if (!interestRate.HasValue)
                    throw new InvalidOperationException(
                        "Interest rate is required for savings accounts.");

                if (!creditFrequency.HasValue)
                    throw new InvalidOperationException(
                        "Credit frequency is required for savings accounts.");

                if (!accrualFrequency.HasValue)
                    throw new InvalidOperationException(
                        "Accrual frequency is required for savings accounts.");

                savingsDetails = SavingsDetails.Create(
                    interestRate.Value,
                    Money.Zero(initialBalance.Currency),
                    creditFrequency.Value,
                    accrualFrequency.Value,
                    DateTime.UtcNow);
            }
            CreditDetails? creditDetails = null;
            if (type == EnAccountType.Credit)
            {
                if (!creditLimit.HasValue)
                    throw new InvalidOperationException(
                        "Credit limit is required for credit accounts.");

                creditDetails = CreditDetails.Create(
                    new Money(
                        creditLimit.Value,
                        initialBalance.Currency),
                    fee.HasValue
                        ? new Money(fee.Value, initialBalance.Currency)
                        : null,
                    feePeriod ?? EnPeriod.None);
            }

            LoanDetails? loanDetails = null;
            if (type == EnAccountType.Loan)
            {
                if (!principalAmount.HasValue)
                    throw new InvalidOperationException(
                        "Principal amount is required for loan accounts.");
                if (!loanInterestRate.HasValue)
                    throw new InvalidOperationException(
                        "Interest rate is required for loan accounts.");
                if (!termInMonths.HasValue)
                    throw new InvalidOperationException(
                        "Term in months is required for loan accounts.");
                if (!repaymentFrequency.HasValue)
                    throw new InvalidOperationException(
                        "Repayment frequency is required for loan accounts.");
                if (!startDate.HasValue)
                    throw new InvalidOperationException(
                        "Start date is required for loan accounts.");
                if (!regularPaymentAmount.HasValue)
                    throw new InvalidOperationException(
                        "Regular payment amount is required for loan accounts.");
                if (!maturityDate.HasValue)
                    throw new InvalidOperationException(
                        "Maturity date is required for loan accounts.");

                loanDetails = LoanDetails.Create(
                    new Money(principalAmount.Value, initialBalance.Currency),
                    loanInterestRate.Value,
                    termInMonths.Value,
                    repaymentFrequency.Value,
                    startDate.Value,
                    new Money(regularPaymentAmount.Value, initialBalance.Currency),
                    maturityDate.Value,
                    nextPaymentDate);
            }

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
                CreditDetails = creditDetails,
                LoanDetails = loanDetails,
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
            [EnAccountType.Credit] = [EnAccountType.Cash, EnAccountType.Savings, EnAccountType.Other],
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
            if (Type != newType)
            {
                if (!AllowedAccountTypeTransitions[Type].Contains(newType))
                {
                    throw new InvalidAccountTypeTransitionException(Id, newType);
                }
            }
        }

        public void UpdateDetails(string name, EnAccountType type)
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
        public void AccrueInterest(DateTime accruedAt)
        {
            EnsureSavingsAccount();

            var lastAccrualAt =
                SavingsDetails!.LastInterestAccrualAt ?? CreatedAt;

            var elapsedDays =
                (accruedAt - lastAccrualAt).TotalDays;

            if (elapsedDays <= 0)
                return;

            var dailyRate =
                SavingsDetails.InterestRate / 365m;

            var interestAmount =
                Balance.Amount * dailyRate * (decimal)elapsedDays;

            if (interestAmount <= 0)
                return;

            var interest = new Money(
                interestAmount,
                Balance.Currency);

            SavingsDetails.AccrueInterest(
                interest,
                accruedAt);

            UpdatedAt = accruedAt;
        }
        public void ClearAccruedInterest()
        {
            EnsureSavingsAccount();
            SavingsDetails?.ClearAccruedInterest();
        }
        public void CreditAccruedInterest(DateTime date)
        {
            EnsureSavingsAccount();
            Balance = SavingsDetails!.InterestAccruedToDate;
            SavingsDetails.CreditAccruedInterest(date);
            UpdatedAt = date;
        }
        public void ChangeInterestRate(decimal interestRate)
        {
            EnsureSavingsAccount();

            SavingsDetails!.ChangeInterestRate(interestRate);

            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeCreditFrequency(
            EnInterestCreditFrequency creditFrequency,
            DateTime currentTime)
        {
            EnsureSavingsAccount();

            SavingsDetails!.ChangeCreditFrequency(
                creditFrequency,
                currentTime);

            UpdatedAt = currentTime;
        }

        public void ChangeAccrualFrequency(
            EnInterestAccrualFrequency accrualFrequency,
            DateTime currentTime)
        {
            EnsureSavingsAccount();

            SavingsDetails!.ChangeAccrualFrequency(
                accrualFrequency,
                currentTime);

            UpdatedAt = currentTime;
        }

        // Loan account orchestration methods
        private void EnsureLoanAccount()
        {
            if (Type != EnAccountType.Loan)
                throw new InvalidOperationException(
                    "Operation is only valid for loan accounts.");

            if (LoanDetails is null)
                throw new InvalidOperationException(
                    "Loan account is missing LoanDetails.");
        }

        /// <summary>
        /// Records a loan payment and updates the next payment date.
        /// </summary>
        public void RecordLoanPayment(Money paymentAmount, DateTime paymentDate)
        {
            if (!IsActive)
                throw new InactiveAccountException(Id, Name);

            EnsureLoanAccount();

            if (!LoanDetails!.IsPaymentDue(paymentDate))
                throw new InvalidLoanOperationException(
                    $"Loan payment is not yet due. Next payment date: {LoanDetails.NextPaymentDate:O}");

            if (paymentAmount.Amount <= 0)
                throw new InvalidTransactionAmountException(paymentAmount);

            if (!HasSufficientBalance(paymentAmount))
                throw new InsufficientBalanceException(Id, paymentAmount, Balance);

            // Apply the payment as a transaction
            ApplyTransaction(paymentAmount, EnTransactionType.Expense);

            // Record the payment in loan details
            LoanDetails.RecordPayment(paymentDate);

            UpdatedAt = paymentDate;
        }

        /// <summary>
        /// Checks if a loan payment is due.
        /// </summary>
        public bool IsLoanPaymentDue(DateTime currentDate)
        {
            EnsureLoanAccount();
            return LoanDetails!.IsPaymentDue(currentDate);
        }

        /// <summary>
        /// Checks if a loan payment is overdue.
        /// </summary>
        public bool IsLoanPaymentOverdue(DateTime currentDate)
        {
            EnsureLoanAccount();
            return LoanDetails!.IsPaymentOverdue(currentDate);
        }

        /// <summary>
        /// Gets the remaining term of the loan in months.
        /// </summary>
        public int GetLoanRemainingTermInMonths(DateTime fromDate)
        {
            EnsureLoanAccount();
            return LoanDetails!.GetRemainingTermInMonths(fromDate);
        }

        /// <summary>
        /// Checks if the loan is fully paid (maturity date reached or passed).
        /// </summary>
        public bool IsLoanFullyPaid(DateTime currentDate)
        {
            EnsureLoanAccount();
            return LoanDetails!.IsLoanFullyPaid(currentDate);
        }

        /// <summary>
        /// Updates the next payment date for the loan.
        /// </summary>
        public void UpdateLoanNextPaymentDate(DateTime newNextPaymentDate)
        {
            EnsureLoanAccount();
            LoanDetails!.UpdateNextPaymentDate(newNextPaymentDate);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Checks if the loan has started.
        /// </summary>
        public bool HasLoanStarted(DateTime currentDate)
        {
            EnsureLoanAccount();
            return LoanDetails!.HasStarted(currentDate);
        }

        public bool HasSufficientBalance(Money amount) => Balance.Amount >= amount.Amount;

        public decimal GetAvailableBalance() => Balance.Amount;

        public bool IsOverdrawn() => Balance.Amount < 0;

    }

}

