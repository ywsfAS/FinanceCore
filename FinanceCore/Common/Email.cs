using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Common;

public sealed class Email : ValueObject
{
    public string Address { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email cannot be empty.", nameof(address));

        if (!address.Contains("@"))
            throw new ArgumentException("Invalid email format.", nameof(address));

        Address = address.Trim();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address.ToLowerInvariant(); // case-insensitive equality
    }

    public override string ToString() => Address;
}
