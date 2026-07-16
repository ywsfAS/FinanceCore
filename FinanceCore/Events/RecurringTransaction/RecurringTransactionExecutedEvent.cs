using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Events.RecurringTransaction
{
    public record RecurringTransactionExecutedEvent(Guid id , Guid accountId , Money amount , DateTime executionTime) : DomainEvent;
}
