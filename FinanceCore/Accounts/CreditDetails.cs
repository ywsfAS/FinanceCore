
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
namespace FinanceCore.Domain.Accounts
{

    public sealed class CreditDetails
    {
        public Money CreditLimit { get; private set; }
        public Money Fee { get; private set; }

        public EnPeriod FeePeriod { get; private set; }

        public DateTime? LastFeeChargedAt { get; private set; }
        public DateTime? NextFeeChargeAt { get; private set; }

        private CreditDetails() { }

        public CreditDetails(
            Money creditLimit,
            Money fee,
            EnPeriod feePeriod,
            DateTime firstFeeChargeAt)
        {
            if (creditLimit.IsGreaterOrEqual(Money.Zero(creditLimit.Currency)))
                throw new ArgumentException(
                    "Credit limit must be greater than zero.",
                    nameof(creditLimit));

            if (fee.IsGreaterOrEqual(Money.Zero(fee.Currency)))
                throw new ArgumentException(
                    "Fee cannot be negative.",
                    nameof(fee));

            if (creditLimit.Currency != fee.Currency)
                throw new ArgumentException(
                    "Fee must use the same currency as the credit limit.",
                    nameof(fee));

            CreditLimit = creditLimit;
            Fee = fee;
            FeePeriod = feePeriod;

            NextFeeChargeAt = firstFeeChargeAt;
        }

        public bool IsFeeDue(DateTime currentDate)
        {
            return Fee.IsGreaterThan(Money.Zero(Fee.Currency))
                && NextFeeChargeAt.HasValue
                && currentDate >= NextFeeChargeAt.Value;
        }

        public void RecordFeeCharge(DateTime chargedAt)
        {
            if (!IsFeeDue(chargedAt))
                throw new InvalidOperationException(
                    "The credit fee is not due.");

            LastFeeChargedAt = chargedAt;
            NextFeeChargeAt = CalculateNextFeeDate(chargedAt);
        }

        public DateTime CalculateNextFeeDate(DateTime fromDate)
        {
            return FeePeriod switch
            {
                EnPeriod.Daily =>
                    fromDate.AddDays(1),

                EnPeriod.Weekly =>
                    fromDate.AddDays(7),

                EnPeriod.Monthly =>
                    fromDate.AddMonths(1),

                EnPeriod.Quarterly =>
                    fromDate.AddMonths(3),

                EnPeriod.Yearly =>
                    fromDate.AddYears(1),

                _ => throw new ArgumentOutOfRangeException(
                    nameof(FeePeriod),
                    FeePeriod,
                    "Unsupported fee period.")
            };
        }
    }
}
