
namespace FinanceCore.Domain.Exceptions
{
    public class RecurringTransactionNotFoundException : DomainException  
    {
        public Guid UserId { get; }
        public Guid Id { get; }
        public RecurringTransactionNotFoundException(Guid userId , Guid id) : base($"RecurringTransaction Not Found with [{id}] for user [{userId}]") {
            UserId = userId;
            Id = id;
        }

    }
    public class RecurringTransactionNotDueException : DomainException  
    {
        public Guid UserId { get; }
        public Guid Id { get; }
        public string Message { get; }
        public RecurringTransactionNotDueException(Guid userId , Guid id , string message = "") : base($"{message} :  RecurringTransaction Not Due with [{id}] for user [{userId}]") {
            UserId = userId;
            Id = id;
            Message = message;
        }

    }
    public class RecurringTransactionNotPausedException : DomainException  
    {
        public Guid Id { get; }
        public string Message { get; }
        public RecurringTransactionNotPausedException(Guid id , string message = "") : base($"{message} :  RecurringTransaction Not Due with [{id}]") {
            Id = id;
            Message = message;
        }

    }
}
