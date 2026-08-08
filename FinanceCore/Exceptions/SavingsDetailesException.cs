
namespace FinanceCore.Domain.Exceptions
{
    public class interestAccuredToDateNullException : DomainException
    {
        public interestAccuredToDateNullException(string message) : base(message) { }
    }
    public class InterestRateNegativeException : DomainException
    {
        public InterestRateNegativeException(string message) : base(message) { }
    }
};
