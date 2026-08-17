using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Exceptions
{
    public class InvalidLoanPrincipalAmountException : DomainException
    {
        public decimal Amount { get; }

        public InvalidLoanPrincipalAmountException(decimal amount)
            : base($"Loan principal amount must be greater than zero. Provided: {amount}")
        {
            Amount = amount;
        }
    }

    public class InvalidLoanInterestRateException : DomainException
    {
        public decimal InterestRate { get; }

        public InvalidLoanInterestRateException(decimal interestRate)
            : base($"Loan interest rate cannot be negative. Provided: {interestRate}")
        {
            InterestRate = interestRate;
        }
    }

    public class InvalidLoanTermException : DomainException
    {
        public int TermInMonths { get; }

        public InvalidLoanTermException(int termInMonths)
            : base($"Loan term in months must be greater than zero. Provided: {termInMonths}")
        {
            TermInMonths = termInMonths;
        }
    }

    public class InvalidLoanPaymentAmountException : DomainException
    {
        public decimal Amount { get; }

        public InvalidLoanPaymentAmountException(decimal amount)
            : base($"Loan regular payment amount must be greater than zero. Provided: {amount}")
        {
            Amount = amount;
        }
    }

    public class InvalidLoanDateRangeException : DomainException
    {
        public DateTime StartDate { get; }
        public DateTime MaturityDate { get; }

        public InvalidLoanDateRangeException(DateTime startDate, DateTime maturityDate)
            : base($"Loan maturity date must be on or after start date. Start: {startDate:O}, Maturity: {maturityDate:O}")
        {
            StartDate = startDate;
            MaturityDate = maturityDate;
        }
    }

    public class LoanCurrencyMismatchException : DomainException
    {
        public EnCurrency PrincipalCurrency { get; }
        public EnCurrency PaymentCurrency { get; }

        public LoanCurrencyMismatchException(EnCurrency principalCurrency, EnCurrency paymentCurrency)
            : base($"Loan regular payment must use the same currency as principal amount. Principal: {principalCurrency}, Payment: {paymentCurrency}")
        {
            PrincipalCurrency = principalCurrency;
            PaymentCurrency = paymentCurrency;
        }
    }

    public class LoanNotDueException : DomainException
    {
        public Guid AccountId { get; }
        public DateTime? NextPaymentDate { get; }

        public LoanNotDueException(Guid accountId, DateTime? nextPaymentDate)
            : base($"Loan payment is not yet due. Next payment date: {nextPaymentDate:O}")
        {
            AccountId = accountId;
            NextPaymentDate = nextPaymentDate;
        }
    }

    public class InvalidLoanOperationException : DomainException
    {
        public InvalidLoanOperationException(string message) : base(message) { }
    }
}
