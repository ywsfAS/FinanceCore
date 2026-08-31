
namespace FinanceCore.Domain.Exceptions
{
    public class InterestAccuredToDateNullException : DomainException
    {
        public InterestAccuredToDateNullException(string message) : base(message) { }
    }
    public class InterestRateNegativeException : DomainException
    {
        public InterestRateNegativeException(string message) : base(message) { }
    }
};
