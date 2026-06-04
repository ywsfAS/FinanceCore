
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Common
{
    public class Money : ValueObject
    {
        public decimal Amount { get; init; }
        public EnCurrency Currency { get; init; }

        public Money(decimal amount, EnCurrency currency)
        {
            if (amount < 0)
                throw new MoneyIsNegativeException();
            if (!Enum.IsDefined(typeof(EnCurrency), currency))
            {
                throw new MoneyCurrencyException();
            }
            Currency = currency;
            Amount = amount;
        }
        private void EnsureSameCurrency(Money other)
        {
            if (Currency != other.Currency)
                throw new CurrencyMismatchException(
                    Currency,
                    other.Currency);
        }
        public Money Add(Money other)
        { 
            EnsureSameCurrency(other);
            return new(Amount + other.Amount, Currency);
        
        }
 
        public Money Subtract(Money other)
        {
            EnsureSameCurrency(other);
            if (other.Amount > Amount)
                throw new MoneySubstructionException();
            return new(Amount - other.Amount, Currency);
        }
        public bool IsGreaterOrEqual(Money other)
        {
            EnsureSameCurrency(other);
            return Amount >= other.Amount;
        }
        public bool IsGreaterThan(Money other)
        {
            EnsureSameCurrency(other);
            return Amount > other.Amount;
        }
        public bool IsLessOrEqual(Money other)
        {
            EnsureSameCurrency(other);
            return Amount <= other.Amount;
        }
        public bool IsLessThan(Money other)
        {
            EnsureSameCurrency(other);
            return Amount < other.Amount;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;

        }
        public static Money Zero(EnCurrency currency) => new(0,currency);
    }

}

