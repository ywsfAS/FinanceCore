using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Accounts;

public sealed class LoanDetails : ValueObject
{
    public Money PrincipalAmount { get; private set; } = null!;
    public decimal InterestRate { get; private set; }
    public int TermInMonths { get; private set; }
    public EnRepaymentFrequency RepaymentFrequency { get; private set; }
    public DateTime StartDate { get; private set; }
    public Money RegularPaymentAmount { get; private set; } = null!;
    public DateTime MaturityDate { get; private set; }
    public DateTime? NextPaymentDate { get; private set; }

    private LoanDetails(
        Money principalAmount,
        decimal interestRate,
        int termInMonths,
        EnRepaymentFrequency repaymentFrequency,
        DateTime startDate,
        Money regularPaymentAmount,
        DateTime maturityDate,
        DateTime? nextPaymentDate)
    {
        ValidatePrincipalAmount(principalAmount);
        ValidateInterestRate(interestRate);
        ValidateTermInMonths(termInMonths);
        ValidateRegularPaymentAmount(regularPaymentAmount);
        ValidateDates(startDate, maturityDate);
        ValidateCurrencyMatch(principalAmount, regularPaymentAmount);

        PrincipalAmount = principalAmount;
        InterestRate = interestRate;
        TermInMonths = termInMonths;
        RepaymentFrequency = repaymentFrequency;
        StartDate = startDate;
        RegularPaymentAmount = regularPaymentAmount;
        MaturityDate = maturityDate;
        NextPaymentDate = nextPaymentDate;
    }

    private LoanDetails() { }

    /// <summary>
    /// Factory method to create new loan details. Computes NextPaymentDate if not provided.
    /// </summary>
    public static LoanDetails Create(
        Money principalAmount,
        decimal interestRate,
        int termInMonths,
        EnRepaymentFrequency repaymentFrequency,
        DateTime startDate,
        Money regularPaymentAmount,
        DateTime maturityDate,
        DateTime? nextPaymentDate = null)
    {
        var nextPayment = nextPaymentDate ?? ComputeNextPaymentDate(startDate, repaymentFrequency);

        return new LoanDetails(
            principalAmount,
            interestRate,
            termInMonths,
            repaymentFrequency,
            startDate,
            regularPaymentAmount,
            maturityDate,
            nextPayment);
    }

    /// <summary>
    /// Factory method to reconstitute loan details from persistence.
    /// </summary>
    public static LoanDetails Load(
        Money principalAmount,
        decimal interestRate,
        int termInMonths,
        EnRepaymentFrequency repaymentFrequency,
        DateTime startDate,
        Money regularPaymentAmount,
        DateTime maturityDate,
        DateTime? nextPaymentDate)
    {
        return new LoanDetails(
            principalAmount,
            interestRate,
            termInMonths,
            repaymentFrequency,
            startDate,
            regularPaymentAmount,
            maturityDate,
            nextPaymentDate);
    }

    /// <summary>
    /// Checks if a payment is due on the given date.
    /// </summary>
    public bool IsPaymentDue(DateTime currentDate)
    {
        return NextPaymentDate.HasValue && currentDate >= NextPaymentDate.Value;
    }

    /// <summary>
    /// Computes the next payment date based on the repayment frequency.
    /// </summary>
    public DateTime CalculateNextPaymentDate(DateTime fromDate)
    {
        return ComputeNextPaymentDate(fromDate, RepaymentFrequency);
    }

    private static DateTime ComputeNextPaymentDate(DateTime fromDate, EnRepaymentFrequency frequency)
    {
        return frequency switch
        {
            EnRepaymentFrequency.Monthly => fromDate.AddMonths(1),
            EnRepaymentFrequency.Quarterly => fromDate.AddMonths(3),
            EnRepaymentFrequency.Annually => fromDate.AddYears(1),
            _ => throw new ArgumentOutOfRangeException(nameof(frequency), frequency, "Unsupported repayment frequency.")
        };
    }

    private static void ValidatePrincipalAmount(Money principalAmount)
    {
        if (principalAmount is null)
            throw new ArgumentNullException(nameof(principalAmount), "Principal amount cannot be null.");

        if (principalAmount.IsLessOrEqual(Money.Zero(principalAmount.Currency)))
            throw new InvalidLoanPrincipalAmountException(principalAmount.Amount);
    }

    private static void ValidateInterestRate(decimal interestRate)
    {
        if (interestRate < 0)
            throw new InvalidLoanInterestRateException(interestRate);
    }

    private static void ValidateTermInMonths(int termInMonths)
    {
        if (termInMonths <= 0)
            throw new InvalidLoanTermException(termInMonths);
    }

    private static void ValidateRegularPaymentAmount(Money regularPaymentAmount)
    {
        if (regularPaymentAmount is null)
            throw new ArgumentNullException(nameof(regularPaymentAmount), "Regular payment amount cannot be null.");

        if (regularPaymentAmount.IsLessOrEqual(Money.Zero(regularPaymentAmount.Currency)))
            throw new InvalidLoanPaymentAmountException(regularPaymentAmount.Amount);
    }

    private static void ValidateDates(DateTime startDate, DateTime maturityDate)
    {
        if (maturityDate < startDate)
            throw new InvalidLoanDateRangeException(startDate, maturityDate);
    }

    private static void ValidateCurrencyMatch(Money principalAmount, Money regularPaymentAmount)
    {
        if (principalAmount.Currency != regularPaymentAmount.Currency)
            throw new LoanCurrencyMismatchException(principalAmount.Currency, regularPaymentAmount.Currency);
    }

    /// <summary>
    /// Returns equality components for value object comparison.
    /// </summary>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return PrincipalAmount;
        yield return InterestRate;
        yield return TermInMonths;
        yield return RepaymentFrequency;
        yield return StartDate;
        yield return RegularPaymentAmount;
        yield return MaturityDate;
        yield return NextPaymentDate ?? default;
    }

    /// <summary>
    /// Checks if the loan is fully paid (maturity date reached or passed).
    /// </summary>
    public bool IsLoanFullyPaid(DateTime currentDate)
    {
        return currentDate >= MaturityDate;
    }

    /// <summary>
    /// Checks if a payment is overdue.
    /// </summary>
    public bool IsPaymentOverdue(DateTime currentDate)
    {
        return NextPaymentDate.HasValue && currentDate > NextPaymentDate.Value;
    }

    /// <summary>
    /// Records a payment by updating NextPaymentDate. Should be called by Account aggregate.
    /// </summary>
    public void RecordPayment(DateTime paymentDate)
    {
        if (!IsPaymentDue(paymentDate))
            throw new LoanNotDueException(Guid.Empty, NextPaymentDate);

        NextPaymentDate = CalculateNextPaymentDate(paymentDate);
    }

    /// <summary>
    /// Updates the next payment date to a specific date. Should be called by Account aggregate for corrections.
    /// </summary>
    public void UpdateNextPaymentDate(DateTime newNextPaymentDate)
    {
        if (newNextPaymentDate <= DateTime.UtcNow)
            throw new InvalidLoanOperationException(
                $"Next payment date must be in the future. Provided: {newNextPaymentDate:O}");

        NextPaymentDate = newNextPaymentDate;
    }

    /// <summary>
    /// Gets the remaining loan term in months from a given date.
    /// </summary>
    public int GetRemainingTermInMonths(DateTime fromDate)
    {
        var monthsDifference = (MaturityDate.Year - fromDate.Year) * 12
                                + (MaturityDate.Month - fromDate.Month);
        return Math.Max(0, monthsDifference);
    }

    /// <summary>
    /// Checks if the loan has started.
    /// </summary>
    public bool HasStarted(DateTime currentDate)
    {
        return currentDate >= StartDate;
    }
}
