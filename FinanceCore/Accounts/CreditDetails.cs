
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
            if (creditLimit.IsLessOrEqual(Money.Zero(creditLimit.Currency)))
                throw new ArgumentException(
                    "Credit limit must be greater than zero.",
                    nameof(creditLimit));

            if (fee.IsLessOrEqual(Money.Zero(fee.Currency)))
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
        public static CreditDetails Create(Money limit , Money? fee = null , EnPeriod period = EnPeriod.None , DateTime? firstFeeChargedAt = null)
        {
            var creditFee = fee ?? Money.Zero(limit.Currency);
            var date = firstFeeChargedAt ?? DateTime.UtcNow;
            return new CreditDetails(limit, creditFee, period, date);
        }
        public static CreditDetails Load(Money creditLimit , Money fee , EnPeriod period , DateTime? lastFeeChargedAt = null , DateTime? nextFeeChargedAt = null)
        {
            return new CreditDetails { CreditLimit = creditLimit, Fee = fee, FeePeriod = period,
                LastFeeChargedAt = lastFeeChargedAt, NextFeeChargeAt = nextFeeChargedAt };
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
