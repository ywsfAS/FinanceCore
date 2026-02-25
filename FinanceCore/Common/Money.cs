
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Common
{
    public class Money : ValueObject
    {
        public decimal Amount { get; init; }

        public Money(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Money amount cannot be negative");
            Amount = amount;
        }

        public Money Add(Money other) => new(Amount + other.Amount);

        public Money Subtract(Money other)
        {
            if (other.Amount > Amount)
                throw new ArgumentException("Cannot subtract more than available amount");
            return new(Amount - other.Amount);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount; // only define the equality here !

        }
        public static Money Zero => new(0);

        public static implicit operator decimal(Money money) => money.Amount;
        public static implicit operator Money(decimal amount) => new(amount);
    }

}

