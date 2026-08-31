using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Accounts;

public sealed class SavingsDetails : ValueObject
{
    public decimal InterestRate { get; private set; }

    public Money InterestAccruedToDate { get; private set; } = null!;

    public DateTime? LastInterestAccrualAt { get; private set; }

    public DateTime NextInterestAccrualAt { get; private set; }

    public EnInterestAccrualFrequency AccrualFrequency { get; private set; }

    public EnInterestCreditFrequency CreditFrequency { get; private set; }

    public DateTime? LastInterestCreditAt { get; private set; }

    public DateTime NextInterestCreditAt { get; private set; }

    private SavingsDetails(
        decimal interestRate,
        Money interestAccruedToDate,
        EnInterestCreditFrequency creditFrequency,
        EnInterestAccrualFrequency accrualFrequency,
        DateTime? lastInterestAccrualAt,
        DateTime nextInterestAccrualAt,
        DateTime? lastInterestCreditAt,
        DateTime nextInterestCreditAt)
    {
        ValidateInterestRate(interestRate);

        if (interestAccruedToDate is null)
            throw new InterestAccuredToDateNullException(
                "Interest accrued to date cannot be null.");

        InterestRate = interestRate;
        InterestAccruedToDate = interestAccruedToDate;

        CreditFrequency = creditFrequency;
        AccrualFrequency = accrualFrequency;

        LastInterestAccrualAt = lastInterestAccrualAt;
        NextInterestAccrualAt = nextInterestAccrualAt;

        LastInterestCreditAt = lastInterestCreditAt;
        NextInterestCreditAt = nextInterestCreditAt;
    }

    private SavingsDetails()
    {
    }

    public static SavingsDetails Create(
        decimal interestRate,
        Money interestAccruedToDate,
        EnInterestCreditFrequency creditFrequency,
        EnInterestAccrualFrequency accrualFrequency,
        DateTime? createdAt = null)
    {
        var date = createdAt ?? DateTime.UtcNow;

        var nextAccrualAt =
            ComputeNextInterestAccrualAt(
                accrualFrequency,
                date);

        var nextCreditAt =
            ComputeNextInterestCreditAt(
                creditFrequency,
                date);

        return new SavingsDetails(
            interestRate,
            interestAccruedToDate,
            creditFrequency,
            accrualFrequency,
            lastInterestAccrualAt: null,
            nextInterestAccrualAt: nextAccrualAt,
            lastInterestCreditAt: null,
            nextInterestCreditAt: nextCreditAt);
    }

    public static SavingsDetails Load(
        decimal interestRate,
        Money interestAccruedToDate,
        EnInterestCreditFrequency creditFrequency,
        EnInterestAccrualFrequency accrualFrequency,
        DateTime? lastInterestAccrualAt,
        DateTime nextInterestAccrualAt,
        DateTime? lastInterestCreditAt,
        DateTime nextInterestCreditAt)
    {
        return new SavingsDetails(
            interestRate,
            interestAccruedToDate,
            creditFrequency,
            accrualFrequency,
            lastInterestAccrualAt,
            nextInterestAccrualAt,
            lastInterestCreditAt,
            nextInterestCreditAt);
    }

    public void ChangeInterestRate(decimal interestRate)
    {
        ValidateInterestRate(interestRate);

        InterestRate = interestRate;
    }

    public void ChangeCreditFrequency(
        EnInterestCreditFrequency creditFrequency,
        DateTime currentTime)
    {
        CreditFrequency = creditFrequency;

        NextInterestCreditAt =
            ComputeNextInterestCreditAt(
                creditFrequency,
                currentTime);
    }

    public void ChangeAccrualFrequency(
        EnInterestAccrualFrequency accrualFrequency,
        DateTime currentTime)
    {
        AccrualFrequency = accrualFrequency;

        NextInterestAccrualAt =
            ComputeNextInterestAccrualAt(
                accrualFrequency,
                currentTime);
    }

    public void AccrueInterest(
        Money amount,
        DateTime accruedAt)
    {
        if (amount is null)
            throw new InterestAccuredToDateNullException(
                "Interest amount cannot be null.");

        InterestAccruedToDate =
            InterestAccruedToDate.Add(amount);

        LastInterestAccrualAt = accruedAt;

        NextInterestAccrualAt =
            ComputeNextInterestAccrualAt(
                AccrualFrequency,
                accruedAt);
    }

    public void CreditAccruedInterest(DateTime creditedAt)
    {
        LastInterestCreditAt = creditedAt;

        NextInterestCreditAt =
            ComputeNextInterestCreditAt(
                CreditFrequency,
                creditedAt);

        ClearAccruedInterest();
    }

    public void ClearAccruedInterest()
    {
        InterestAccruedToDate =
            Money.Zero(InterestAccruedToDate.Currency);
    }

    private static void ValidateInterestRate(decimal interestRate)
    {
        if (interestRate < 0 || interestRate > 1)
        {
            throw new InterestRateNegativeException(
                $"Interest rate must be between 0 and 1 [{interestRate}]");
        }
    }

    private static DateTime ComputeNextInterestCreditAt(
        EnInterestCreditFrequency frequency,
        DateTime currentTime)
    {
        return frequency switch
        {
            EnInterestCreditFrequency.Monthly =>
                currentTime.AddMonths(1),

            EnInterestCreditFrequency.Quarterly =>
                currentTime.AddMonths(3),

            EnInterestCreditFrequency.Yearly =>
                currentTime.AddYears(1),

            _ => throw new ArgumentOutOfRangeException(
                nameof(frequency),
                frequency,
                "Unsupported interest credit frequency.")
        };
    }

    private static DateTime ComputeNextInterestAccrualAt(
        EnInterestAccrualFrequency frequency,
        DateTime currentTime)
    {
        return frequency switch
        {
            EnInterestAccrualFrequency.Daily =>
                currentTime.AddDays(1),

            EnInterestAccrualFrequency.Monthly =>
                currentTime.AddMonths(1),

            EnInterestAccrualFrequency.Quarterly =>
                currentTime.AddMonths(3),

            EnInterestAccrualFrequency.Yearly =>
                currentTime.AddYears(1),

            _ => throw new ArgumentOutOfRangeException(
                nameof(frequency),
                frequency,
                "Unsupported interest accrual frequency.")
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return InterestRate;
        yield return InterestAccruedToDate;
        yield return AccrualFrequency;
        yield return CreditFrequency;
        yield return LastInterestAccrualAt;
        yield return NextInterestAccrualAt;
        yield return LastInterestCreditAt;
        yield return NextInterestCreditAt;
    }
}