using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using System.Net.NetworkInformation;

namespace FinanceCore.Domain.Accounts;

public sealed class SavingsDetails : ValueObject
{
    public decimal InterestRate { get; private set; }

    public Money InterestAccruedToDate { get; private set; } = null!;

    public DateTime? LastInterestAccrualAt { get; private set; }

    public EnInterestCreditFrequency CreditFrequency { get; private set; }

    public DateTime? NextInterestCreditAt { get; private set; }

    private SavingsDetails(
        decimal interestRate,
        Money interestAccruedToDate,
        EnInterestCreditFrequency creditFrequency,
        DateTime? lastInterestAccrualAt,
        DateTime? nextInterestCreditAt)
    {
        ValidateInterestRate(interestRate);

        if (interestAccruedToDate is null)
            throw new InterestAccuredToDateNullException(
                "Interest accrued to date cannot be null.");

        InterestRate = interestRate;
        InterestAccruedToDate = interestAccruedToDate;
        CreditFrequency = creditFrequency;
        LastInterestAccrualAt = lastInterestAccrualAt;
        NextInterestCreditAt = nextInterestCreditAt;
    }
    private SavingsDetails() { }

    public static SavingsDetails Create(
        decimal interestRate,
        Money interestAccruedToDate,
        EnInterestCreditFrequency creditFrequency,
        DateTime createdAt)
    {
        var nextCreditAt = ComputeNextInterestCreditAt(
            creditFrequency,
            createdAt);

        return new SavingsDetails(
            interestRate,
            interestAccruedToDate,
            creditFrequency,
            lastInterestAccrualAt: null,
            nextInterestCreditAt: nextCreditAt);
    }
    public static SavingsDetails Load(decimal interestRate , Money interestAccruedToDate , EnInterestCreditFrequency creditFrequency , 
        DateTime? lastInterestAccrualAt , DateTime? nextInterestCreditAt)
    {
        return new SavingsDetails
        {
            InterestRate = interestRate,
            InterestAccruedToDate = interestAccruedToDate,
            CreditFrequency = creditFrequency,
            LastInterestAccrualAt = lastInterestAccrualAt,
            NextInterestCreditAt = nextInterestCreditAt
        };
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
        NextInterestCreditAt = ComputeNextInterestCreditAt(
            creditFrequency,
            currentTime);
    }

    public void AccrueInterest(
        Money amount,
        DateTime accruedAt)
    {
        if (amount is null)
            throw new InterestAccuredToDateNullException(
                "Interest amount cannot be null.");

        InterestAccruedToDate = InterestAccruedToDate.Add(amount);
        LastInterestAccrualAt = accruedAt;
    }

    public void ClearAccruedInterest()
    {
        InterestAccruedToDate =
            Money.Zero(InterestAccruedToDate.Currency);
    }

    public void AdvanceNextInterestCredit(DateTime creditAt)
    {
        NextInterestCreditAt = ComputeNextInterestCreditAt(
            CreditFrequency,
            creditAt);
    }

    private static void ValidateInterestRate(decimal interestRate)
    {
        if (interestRate < 0)
        {
            throw new InterestRateNegativeException(
                $"Interest rate cannot be negative [{interestRate}]");
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return InterestRate;
        yield return InterestAccruedToDate;
        yield return CreditFrequency;
        yield return LastInterestAccrualAt;
        yield return NextInterestCreditAt;
    }
}