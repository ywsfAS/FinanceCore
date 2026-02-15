
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Common;

public sealed class Money : ValueObject
{
    public decimal Amount { get; }
    public EnCurrency Currency { get; }

    public Money(decimal amount, EnCurrency currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.");


        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);

        if (Amount < other.Amount)
            throw new InvalidOperationException("Insufficient funds.");

        return new Money(Amount - other.Amount, Currency);
    }

    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException("Currency mismatch.");
    }
}
