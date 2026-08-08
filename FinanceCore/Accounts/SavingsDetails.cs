using FinanceCore.Domain.Common;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Domain.Accounts
{
    public class SavingsDetails : ValueObject
    {
        public decimal InterestRate { get; private set; }
        public Money InterestAccruedToDate { get; private set; } = null!;

        public SavingsDetails(decimal rate , Money interestAccuredToDate)
        {
            if (rate < 0) throw new InterestRateNegativeException($"Interest rate cannot be negative [{rate}]");
            if (interestAccuredToDate is null) throw new interestAccuredToDateNullException("InterestAccuredToDate cannot be null");

            InterestRate = rate;
            InterestAccruedToDate = interestAccuredToDate;
        }
        public void ChangeInterestRate(decimal rate)
        {
            if (rate < 0) throw new InterestRateNegativeException($"Interest rate cannot be negative [{rate}]");
            InterestRate = rate;
        }
        public void AccrueInterest(Money amount)
        {
            if (amount is null) throw new interestAccuredToDateNullException("InterestAccuredToDate cannot be null");
            InterestAccruedToDate.Add(amount);
        }

        public void ClearAccruedInterest()
        {
            InterestAccruedToDate = Money.Zero(InterestAccruedToDate.Currency);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return InterestRate;
            yield return InterestAccruedToDate;
        }
    }
}
