
namespace FinanceCore.Domain.Exceptions
{
    public class MoneyIsNegativeException : DomainException
    {
        public MoneyIsNegativeException() : base("Money Can't Be Neagitve") { }
    }
    public class MoneySubstructionException : DomainException
    {
        public MoneySubstructionException() : base("Cannot subtract more than available amount") { }
    }
    public class MoneyCurrencyException : DomainException
    {
        public MoneyCurrencyException() : base("Invalid Currency") { }
    }

}
